using Microsoft.AspNetCore.Identity;

namespace ProniaOnion.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
