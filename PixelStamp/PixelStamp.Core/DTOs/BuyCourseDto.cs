using PixelStamp.Core.Entities;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Dtos
{
    public class BuyCourseDto
    {
        public int CourseId { get; set; }
        public Guid UserId { get; set; }
    }
   
}
