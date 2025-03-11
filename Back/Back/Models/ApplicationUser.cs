using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Auth1.Models
{
    public class ApplicationUser : IdentityUser
    {
        // You don't need this property
        // public string Role { get; set; }
    }

}
