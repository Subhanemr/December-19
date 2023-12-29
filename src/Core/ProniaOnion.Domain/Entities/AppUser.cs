using Microsoft.AspNetCore.Identity;
using System.Diagnostics.SymbolStore;

namespace ProniaOnion.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public bool IsActivate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireAt { get; set; }
    }
}
