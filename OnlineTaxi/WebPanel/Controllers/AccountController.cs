using Domain.DTO.Account;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebPanel.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _context;

        public AccountController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var res = new LoginDTO()
            {
                ReturnUrl = ReturnUrl
            };

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context._user.GetByUsernameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "کاربر پیدا نشد");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError("", "رمز عبور اشتباه است");
                return View(model);
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("Username", model.Username));
            claims.Add(new Claim("Id", user.Id.ToString()));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var princple = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princple,
                new AuthenticationProperties() { IsPersistent = false });

            if (model.ReturnUrl != null)
                return LocalRedirect(model.ReturnUrl);
            else
                return RedirectToAction("Index", "Home");


        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
