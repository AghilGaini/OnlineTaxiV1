using Domain.DTO.Security;
using Domain.Entities;
using Domain.Interfaces;
using EfCoreDAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreDAL.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRoleDomain>, IUserRoleDomain
    {
        private readonly ApplicationContext _context;

        public UserRoleRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAllRolesByUserId(long userId)
        {
            try
            {
                var res = await _context.UserRoles.Where(r => r.UserId == userId).ToListAsync();
                _context.RemoveRange(res);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<long>> GetAllRoleIdsByUserIdAsync(long userId)
        {
            return await _context.UserRoles.Where(r => r.UserId == userId).Select(r => r.RoleId).ToListAsync();
        }

        public async Task<bool> InsertUserRoleRangeAsync(UserRoleDTO model)
        {
            try
            {
                var newUserRole = new List<UserRoleDomain>();
                foreach (var item in model.UserRoles)
                {
                    newUserRole.Add(new UserRoleDomain()
                    {
                        RoleId = item.Id,
                        UserId = model.UserId
                    });
                }

                await _context.UserRoles.AddRangeAsync(newUserRole);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
