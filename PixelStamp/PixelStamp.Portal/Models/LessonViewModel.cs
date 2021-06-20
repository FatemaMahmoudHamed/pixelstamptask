using Microsoft.AspNetCore.Http;

namespace PixelStamp.Portal.Models
{
    public class LessonViewModel
    {
        public int CourseId { get; set; }

        public string Description { get; set; }

        public IFormFile Video { get; set; }

    }
}
