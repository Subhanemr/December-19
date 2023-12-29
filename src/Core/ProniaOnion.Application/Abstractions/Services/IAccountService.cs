using ProniaOnion.Application.Dtos.Account;
using ProniaOnion.Application.Dtos.Token;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto register);
        Task<TokenResponseDto> LogInAsync(LoginDto login);

        Task<TokenResponseDto> LogInByRefreshToken(string refresh);
    }
}
    