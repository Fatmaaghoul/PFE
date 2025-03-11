using Back.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doc.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : Controller
    {
        private readonly DocContext _docContext;
        public ImageController(DocContext docContext)
        {
            _docContext = docContext;
        }

        [HttpGet("{docId}")] 
        public async Task<IActionResult> GetAllImages(Guid docId)
        {
            
            var images = await _docContext.Images
                .Where(i => i.DocumentId == docId)
                .ToListAsync();

            if (images == null || !images.Any())
            {
                return NotFound("No images found for the given document ID");
            }

            return Ok(images);
        }
    }
}
