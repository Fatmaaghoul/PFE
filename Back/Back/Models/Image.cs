using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{
    public class DocumentImage
    {
        public Guid Id { get; set; }
        public string FileUrl { get; set; }
        public string? Description { get; set; }
        public Guid DocumentId { get; set; }
        [ForeignKey("DocumentId")]
        public Document? Document { get; set; }
    }
}
