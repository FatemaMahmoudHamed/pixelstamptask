using PixelStamp.Core.Entities;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Dtos
{
    public class LessonDto
    {
        public int? Id { get; set; }

        public int CourseId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

    }
}
