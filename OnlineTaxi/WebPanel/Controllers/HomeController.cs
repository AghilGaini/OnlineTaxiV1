using Domain.DTO.Home;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using CoreService;

namespace WebPanel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _context;
        public HomeController(IUnitOfWork context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var res = new PersonDTO();

            res.PersonsInfo.AddRange(await _context._person.GetAllDTOAsync());

            res.Actions.Add(new ActionItems() { Title = "Edit", Action = "Edit", Controller = "Home" });
            res.Actions.Add(new ActionItems() { Title = "Update", Action = "UpdatePerson", Controller = "Home" });

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePerson(long id)
        {
            var person = await _context._person.GetByIdAsync(id);
            if (person == null)
            {
                ModelState.AddModelError("", "شخصی پیدا نشد");
                return View();
            }

            var res = new UpdatePersonDTO()
            {
                Id = id,
                Name = person.Name
            };

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePerson(UpdatePersonDTO model)
        {
            var oldPerson = await _context._person.GetByIdAsync(model.Id);
            if (oldPerson == null)
            {
                ModelState.AddModelError("", "شخصی پیدا نشد");
                return View(model);
            }

            if (await _context._person.UpdatePersonDTOAsync(model))
            {
                _context.Complete();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "خطا در عملیات");
                return View(model);
            }

        }

    }
}
