using Domain.DTO.Account;

namespace WebApi.Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, TokenDTO user);
        bool ValidateToken(string key, string issuer, string audience, string token);

    }
}
