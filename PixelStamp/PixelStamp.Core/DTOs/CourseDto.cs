using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }

        public Guid? CourseOwner { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<UserDto> Buyers { get; set; }

        public ICollection<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }
}
