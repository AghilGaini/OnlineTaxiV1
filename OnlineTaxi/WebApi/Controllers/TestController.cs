using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        private const string tokenPrefix = "Bearer ";

        public TestController(IUnitOfWork context,
            ITokenService tokenService, IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ApiResponse> Test1()
        {
            var res = new ApiResponse() { Success = false };

            if (HttpContext.Request.Headers.TryGetValue("Authorization",
                out Microsoft.Extensions.Primitives.StringValues headerValue))
            {
                string token = headerValue;

                if (!string.IsNullOrEmpty(token) && token.StartsWith(tokenPrefix))
                {
                    token = token.Substring(tokenPrefix.Length);
                }
                else
                {
                    res.ErrorMessage = $"Token Header is invalid : {token}";
                }

                if (!_tokenService.ValidateToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"], token))
                {
                    res.ErrorMessage = $"Token is invalid";
                }
                else
                {
                    res.Success = true;
                }
            }
            else
            {
                res.ErrorMessage = "Can not retrive token ";
            }

            await Task.CompletedTask;
            return res;
        }

        #region models

        public class Test1model
        {
            public string str { get; set; }
        }

        #endregion


    }
}
