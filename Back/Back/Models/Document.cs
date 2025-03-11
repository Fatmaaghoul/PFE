using Auth1.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Back.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime UploadDate { get; set; }
        public string FileUrl { get; set; } = "";
        public string? Text { get; set; }
        public ICollection<DocumentImage>? Images { get; set; }

        //public Guid? UserId { get; set; } 
        public string UserId { get; set; } // Utiliser string au lieu de Guid pour correspondre à IdentityUser

        [ForeignKey("UserId")]
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
    }
}
