using Domain.DTO.Security;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Model;
using System.Linq;

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
