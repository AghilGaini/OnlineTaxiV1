using Domain.DTO.Security;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Model;

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
            res.Actions.Add(new ActionItems() { Title = "مدیریت دسترسی", Action = "RolePermision", Controller = "Security" });
            res.Actions.Add(new ActionItems() { Title = "حذف", Action = "Delete", Controller = "Security" });


            return View(res);
        }
    }
}
