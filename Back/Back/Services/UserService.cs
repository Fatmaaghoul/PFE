using Microsoft.AspNetCore.Identity;
using Auth1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth1.Services
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

        // ✅ 1. Ajouter un utilisateur avec rôle
        public async Task<ResponseModel> AddUserAsync(string username, string email, string password, List<string> roles)
        {
            var user = new ApplicationUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return new ResponseModel { Success = false, Message = "User creation failed!" };

            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            return new ResponseModel { Success = true, Message = "User created successfully!" };
        }

        // ✅ 2. Modifier un utilisateur (email, username, rôles)
        public async Task<ResponseModel> EditUserAsync(string userId, string username, string email, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new ResponseModel { Success = false, Message = "User not found!" };

            user.UserName = username;
            user.Email = email;
            await _userManager.UpdateAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles); // Supprimer les anciens rôles
            await _userManager.AddToRolesAsync(user, roles); // Ajouter les nouveaux rôles

            return new ResponseModel { Success = true, Message = "User updated successfully!" };
        }

        // ✅ 3. Supprimer un utilisateur
        public async Task<ResponseModel> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new ResponseModel { Success = false, Message = "User not found!" };

            await _userManager.DeleteAsync(user);
            return new ResponseModel { Success = true, Message = "User deleted successfully!" };
        }

        // ✅ 4. Récupérer la liste des utilisateurs avec leurs rôles
        public async Task<List<UserResponseModel>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userList = new List<UserResponseModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return userList;
        }
    }

  

    public class UserResponseModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
