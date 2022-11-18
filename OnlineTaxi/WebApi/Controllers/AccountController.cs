using Domain.DTO.Account;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AccountController(IUnitOfWork context,
            ITokenService tokenService, IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ApiResponse> Login(LoginDTO model)
        {
            try
            {
                var user = await _context._user.GetByUsernameAsync(model.Username);

                if (user == null)
                    throw new System.Exception("User not found");

                if (user.Password != model.Password)
                    throw new System.Exception("Password is inocrrect");

                var token = _tokenService.BuildToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], model);

                return new ApiResponse()
                {
                    Data = token,
                    ResponseCode = 0,
                    Success = true
                };

            }
            catch (System.Exception ex)
            {
                return new ApiResponse()
                {
                    Data = null,
                    ResponseCode = -1,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }

        }
    }
}
