using Domain.DTO.Account;

namespace WebApi.Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, LoginDTO user);
        bool ValidateToken(string key, string issuer, string audience, string token);

    }
}
