using Domain.DTO.Account;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ITokenService tokenService,
            IConfiguration configuration,
            IUnitOfWork context)
        {
            _logger = logger;
            _tokenService = tokenService;
            _config = configuration;
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var adminUser = await _context._user.GetByUsernameAsync("admin");

            var userDto = new LoginDTO()
            {
                Id = adminUser.Id,
                Username = adminUser.Username,
            };

            string token = "";
            if (adminUser != null)
                token = _tokenService.BuildToken(_config["Jwt:Key"], _config["Jwt:Issuer"], userDto);

            bool isValidToken = _tokenService.ValidateToken(_config["Jwt:Key"], _config["Jwt:Issuer"],
                _config["Jwt:Issuer"], token);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
