using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPanel.Filters
{
    public class CustomAuthorization : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _permision;
        private readonly string _roles;

        private static IUnitOfWork _unitOfWork;
        private static List<UserDomain> _databaseUsers = new List<UserDomain>();
        private static List<RoleDomain> _databaseRoles = new List<RoleDomain>();
        private static List<PermisionDomain> _databasePermisions = new List<PermisionDomain>();
        private static List<UserRoleDomain> _databaseUserRoles = new List<UserRoleDomain>();
        private static List<RolePermisionDomain> _databaseRolePermisions = new List<RolePermisionDomain>();
        public CustomAuthorization(string permision, string roles)
        {
            _permision = permision;
            _roles = roles;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
                return;

            if (context.HttpContext.User == null)
                return;

            if (context.HttpContext.User.Identity.Name == null)
                return;

            if (_unitOfWork == null)
                _unitOfWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

            #region Initials

            if (_databaseUsers.Count == 0)
            {
                _databaseUsers.AddRange(await _unitOfWork._user.GetAllAsync());
            }

            if (_databaseRoles.Count == 0)
            {
                _databaseRoles.AddRange(await _unitOfWork._role.GetAllAsync());
            }

            if (_databasePermisions.Count == 0)
            {
                _databasePermisions.AddRange(await _unitOfWork._permision.GetAllAsync());
            }

            if (_databaseUserRoles.Count == 0)
            {
                _databaseUserRoles.AddRange(await _unitOfWork._userRole.GetAllAsync());
            }

            if (_databaseRolePermisions.Count == 0)
            {
                _databaseRolePermisions.AddRange(await _unitOfWork._rolePermision.GetAllAsync());
            }

            #endregion

            var user = _databaseUsers.FirstOrDefault(r => r.Username == context.HttpContext.User.Identity.Name);
            if (user == null)
                return;

            if (user.IsAdmin)
                return;

            //get RoleIDs
            var userRoleIds = _databaseUserRoles.Where(r => r.UserId == user.Id).Select(r => r.RoleId).ToList();

            if (userRoleIds == null || userRoleIds.Count == 0)
                return;

            //get roles of user
            var roles = _databaseRoles.Where(r => userRoleIds.Contains(r.Id)).ToList();

            //get rolePermisions
            var rolePermisions = _databaseRolePermisions.Where(r => roles.Select(x => x.Id).Contains(r.RoleId)).Select(r => r.PermisionId).ToList();

            if (rolePermisions == null || rolePermisions.Count == 0)
                return;

            //get permisions
            var permisions = _databasePermisions.Where(r => rolePermisions.Contains(r.Id)).ToList();
            if (permisions == null || permisions.Count == 0)
                return;

            if (!permisions.Any(r => r.Value == _permision))
                context.Result = new ForbidResult();

            if (_roles.Trim(',') != string.Empty)
            {
                var arrRoles = _roles.Split(",");

                foreach (var item in arrRoles)
                {
                    if (!roles.Any(r => r.Title.ToLower() == item.ToLower()))
                    {
                        context.Result = new ForbidResult();
                        break;
                    }
                }
            }

            return;
        }
    }
}
