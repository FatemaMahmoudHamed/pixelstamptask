using Microsoft.AspNetCore.Identity;
using PixelStamp.Core.Dtos;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Entities
{
    public class Exam 
    {
        public int Id { get; set; }

        /// <summary>
        /// The creation datetime.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
        
        public ICollection<Question> Questions { get; set; }

    }
}
