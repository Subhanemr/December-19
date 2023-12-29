using ProniaOnion.Application.Dtos.Token;
using ProniaOnion.Domain.Entities;
using System.Security.Claims;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateJwt(AppUser user, ICollection<Claim> claims,int minutes);
    }
}
