using Domain.DTO.Account;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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

                var token = _tokenService.BuildToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], new TokenDTO() { Id = user.Id, Username = user.Username });

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

        [HttpPost]
        public async Task<ApiResponse> Register(RegisterDTO model)
        {
            try
            {
                var user = await _context._user.GetByUsernameAsync(model.Username);

                if (user != null)
                    throw new System.Exception("user exist");

                if (model.Password != model.ConfirmPassword)
                    throw new System.Exception("Password is not same with Confirm Password");

                var newUser = new UserDomain()
                {
                    IsAdmin = false,
                    Password = model.Password,
                    Username = model.Username,
                    UserType = 0
                };

                if (await _context._user.AddAsync(newUser))
                {
                    _context.Complete();

                    newUser = await _context._user.GetByUsernameAsync(newUser.Username);

                    if (newUser == null)
                        throw new System.Exception("System exception.user not found");

                    var tokenModel = new TokenDTO()
                    {
                        Id = newUser.Id,
                        Username = newUser.Username
                    };

                    var token = _tokenService.BuildToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], tokenModel);

                    return new ApiResponse()
                    {
                        Data = token,
                        ResponseCode = 0,
                        Success = true
                    };
                }
                else
                {
                    throw new System.Exception("System exception in register");
                }

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
