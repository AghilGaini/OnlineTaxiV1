using Domain.DTO.Security;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Model;
using System.Linq;
using WebPanel.Filters;
using CoreService;

namespace WebPanel.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IUnitOfWork _context;

        public SecurityController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [CustomAuthorization(PermisionManager.Permisions.Security_Users_HttpGet, "")]
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var res = new UserDTO();

            res.Users.AddRange(await _context._user.GetAllUsersDTOAsync());

            res.Actions.Add(new ActionItems() { Title = "ویرایش", Action = "Edit", Controller = "Security" });
            res.Actions.Add(new ActionItems() { Title = "مدیریت دسترسی", Action = "UserRole", Controller = "Security" });
            res.Actions.Add(new ActionItems() { Title = "حذف", Action = "Delete", Controller = "Security" });

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> UserRole(long Id)
        {
            var res = new UserRoleDTO();

            var user = await _context._user.GetByIdAsync(Id);
            if (user == null)
            {
                ModelState.AddModelError("", "کاربری پیدا نشد");
                return View(res);
            }

            res.UserRoles.AddRange(await _context._role.GetAllDTOAsync());
            res.UserId = user.Id;

            var userRoleIds = await _context._userRole.GetAllRoleIdsByUserIdAsync(user.Id);

            foreach (var item in res.UserRoles)
            {
                if (userRoleIds.Any(r => r == item.Id))
                    item.IsSelected = true;
            }

            ViewBag.UserTitle = user.Username;

            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> UserRole(UserRoleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context._user.GetByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "کاربری پیدا نشد");
                return View(model);
            }

            if (await _context._userRole.DeleteAllRolesByUserId(user.Id))
            {
                var newUserRole = new UserRoleDTO();
                foreach (var item in model.UserRoles)
                {
                    if (item.IsSelected)
                        newUserRole.UserRoles.Add(item);
                }
                newUserRole.UserId = user.Id;

                if (await _context._userRole.InsertUserRoleRangeAsync(newUserRole))
                {
                    _context.Complete();

                    return RedirectToAction("Users", "Security");
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var res = new RoleDTO();

            res.Roles.AddRange(await _context._role.GetAllDTOAsync());

            res.Actions.Add(new ActionItems() { Title = "ویرایش", Action = "Edit", Controller = "Security" });
            res.Actions.Add(new ActionItems() { Title = "مدیریت دسترسی", Action = "Permisions", Controller = "Security" });
            res.Actions.Add(new ActionItems() { Title = "حذف", Action = "Delete", Controller = "Security" });

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Permisions(long Id)
        {
            var res = new PermisionDTO();

            res.Permisions.AddRange(await _context._permision.GetAllDTOAsync());

            var role = await _context._role.GetByIdAsync(Id);

            if (role == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View(res);
            }

            var rolePermisionId = await _context._rolePermision.GetAllPermisionsIdByRoleIdAsync(role.Id);

            foreach (var item in res.Permisions)
            {
                if (rolePermisionId.Any(r => r == item.Id))
                    item.IsSelected = true;
            }

            res.RoleId = role.Id;
            ViewBag.RoleTitle = role.Title;

            return View(res);

        }
        [HttpPost]
        public async Task<IActionResult> Permisions(PermisionDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var role = await _context._role.GetByIdAsync(model.RoleId);
            if (role == null)
            {
                ModelState.AddModelError("", "نقشی پیدا نشد");
                return View(model);
            }

            if (await _context._rolePermision.DeletePermisionsByRoleIdAsync(role.Id))
            {
                var newRolePermison = new PermisionDTO();
                foreach (var item in model.Permisions)
                {
                    if (item.IsSelected)
                        newRolePermison.Permisions.Add(item);
                }
                newRolePermison.RoleId = role.Id;

                if (await _context._rolePermision.InsertRolePermisionRangeDTOAsync(newRolePermison))
                {
                    _context.Complete();

                    return RedirectToAction("Roles", "Security");
                }
            }

            return View(model);
        }
    }
}
