using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        /// <summary>
        /// The user's id who created it.
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// The creation datetime.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// The user's id who updated it.
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        // <summary>
        /// The update datetime.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Logical delete, if true don't show it.
        /// </summary>
        public bool IsDeleted { get; set; }
        public bool Enabled { get; set; }=true;

        public Decimal Budget { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();

        public ICollection<Course> Courses { get; set; } = new List<Course>();


        [JsonIgnore]
        public ICollection<AppUserClaim> UserClaims { get; set; } = new List<AppUserClaim>();


    }
}
