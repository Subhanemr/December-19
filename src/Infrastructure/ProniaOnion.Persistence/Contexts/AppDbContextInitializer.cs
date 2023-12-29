using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProniaOnion.Domain.Entities;
using ProniaOnion.Domain.Enums;

namespace ProniaOnion.Persistence.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AppDbContextInitializer(RoleManager<IdentityRole> roleManager, IConfiguration configuration, UserManager<AppUser> userManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        public async Task InitializeDbContext()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task CreateUserRoles()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
        }

        public async Task InitializeAdmin()
        {
            AppUser admin = new AppUser
            {
                Name = "admin",
                Surname = "admin",
                Email = _configuration["AdminSettings:Email"],
                UserName = _configuration["AdminSettings:UserName"]
            };

            await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
            await _userManager.AddToRoleAsync(admin, UserRoles.Admin.ToString());
        }
    }
}
