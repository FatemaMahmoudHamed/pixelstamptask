using Microsoft.AspNetCore.Identity;
using PixelStamp.Core.Dtos;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Entities
{
    public class ExamDto
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CourseId { get; set; }

        public List<int> Questions { get; set; }

    }
}
