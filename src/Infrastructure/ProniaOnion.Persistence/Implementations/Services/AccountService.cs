using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Account;
using ProniaOnion.Application.Dtos.Token;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Domain.Enums;
using System.Security.Claims;
using System.Text;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<TokenResponseDto> LogInAsync(LoginDto login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UserNameOrEmail);
                if (user == null) throw new Exception("Username, Email or Password is incorrect");
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
            if (result.IsLockedOut) throw new Exception("Login is not enable please try latter");
            if (!result.Succeeded) throw new Exception("Username, Email or Password is incorrect");

            ICollection<Claim> claims = await _userClaims(user);

            var tokenResponse = _tokenHandler.CreateJwt(user, claims ,60);
            user.RefreshToken = tokenResponse.RefreshToken;
            user.RefreshTokenExpireAt = tokenResponse.RefreshTokenExpire;
            await _userManager.UpdateAsync(user);

            return tokenResponse;
        }

        public async Task RegisterAsync(RegisterDto register)
        {
            AppUser user = _mapper.Map<AppUser>(register);

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            StringBuilder message = new StringBuilder();
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    message.AppendLine(error.Description);
                }
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
        }

        public async Task<TokenResponseDto> LogInByRefreshToken(string refresh)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refresh);
            if (user == null) throw new Exception("Not found");
            if (user.RefreshTokenExpireAt < DateTime.UtcNow) throw new Exception("Token is Expired");

            var tokenResponse = _tokenHandler.CreateJwt(user, await _userClaims(user), 60);
            user.RefreshToken = tokenResponse.RefreshToken;
            user.RefreshTokenExpireAt = tokenResponse.RefreshTokenExpire;
            await _userManager.UpdateAsync(user);

            return tokenResponse;

        }

        private async Task<ICollection<Claim>> _userClaims(AppUser user)
        {
            ICollection<Claim> claims =  new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach(var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
