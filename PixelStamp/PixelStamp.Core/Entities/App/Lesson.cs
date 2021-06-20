using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        /// <summary>
        /// The creation datetime.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
