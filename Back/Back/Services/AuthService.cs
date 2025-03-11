using Auth1.Models;
using Back.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auth1.Services
{
    public class AuthService
    {
        private readonly DocContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(DocContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ResponseModel Register(RegisterModel model)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == model.UserName || u.Email == model.Email);
            if (existingUser != null)
            {
                return new ResponseModel { Success = false, Message = "User already exists!" };
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                return new ResponseModel { Success = true, Message = "Registration successful!" };
            }

            return new ResponseModel { Success = false, Message = "Error during registration." };
        }

        public ResponseModel Login(LoginModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return new ResponseModel { Success = false, Message = "Invalid credentials!" };
            }

            return new ResponseModel { Success = true, Message = "Login successful!" };
        }

      

      
    }
}
