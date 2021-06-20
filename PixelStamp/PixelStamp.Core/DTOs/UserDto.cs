using PixelStamp.Core.Entities;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; } = "12345678";

        public string Email { get; set; }

        public Decimal Budget { get; set; }

        public ICollection<CourseDto> Courses { get; set; } = new List<CourseDto>();

        public ICollection<string> Roles { get; set; } = new List<string>();
        public ICollection<string> Claims { get; set; } = new List<string>();
    }

}
