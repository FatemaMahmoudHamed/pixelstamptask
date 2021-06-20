using Microsoft.AspNetCore.Identity;
using PixelStamp.Core.Dtos;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Entities
{
    public class Course 
    {
        public int Id { get; set; }

        public Guid CourseOwner { get; set; }

        /// <summary>
        /// The creation datetime.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<AppUser> Buyers { get; set; }

        public Exam Exam { get; set; }

        public Decimal Price { get; set; }

    }
}
