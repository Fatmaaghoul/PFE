using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Auth1.Controllers
{

    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(UserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;

        }

        // ✅ 1. Ajouter un utilisateur avec un ou plusieurs rôles
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest model)
        {
            var result = await _userService.AddUserAsync(model.Username, model.Email, model.Password, model.Roles);
            return Ok(result);
        }

        // ✅ 2. Modifier un utilisateur et ses rôles
        [HttpPut("edit/{userId}")]
        public async Task<IActionResult> EditUser(string userId, [FromBody] EditUserRequest model)
        {
            var result = await _userService.EditUserAsync(userId, model.Username, model.Email, model.Roles);
            return Ok(result);
        }

        // ✅ 3. Supprimer un utilisateur
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            return Ok(result);
        }

        // ✅ 4. Récupérer tous les utilisateurs avec leurs rôles
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return Ok(roles);
        }


    }

    public class AddUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }

    public class EditUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
