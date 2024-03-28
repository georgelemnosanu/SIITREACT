using Microsoft.AspNetCore.Identity;
using SIITREACT.Model;

namespace SIITREACT.Service
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        public async Task<ApplicationUser> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            return null;
        }

        public async Task<IdentityResult> CreateUser(string username, string email, string password, string role)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!await _userManager.IsInRoleAsync(user, role))
                {
                    
                    var roleExists = await _roleManager.RoleExistsAsync(role);
                    if (roleExists)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                    else
                    {
                        throw new Exception("role not applicable");
                    }
                }
            }

            return result;
        }
    }
}