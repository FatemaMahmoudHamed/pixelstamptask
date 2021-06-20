using System;
using System.Collections.Generic;

namespace PixelStamp.Core.Models
{
    /// <summary>
    /// Used to capture the current logged in user information.
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// User's ID.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's personal computer ID.
        /// </summary>
        public string PcName { get; set; }

        /// <summary>
        /// User's IP address.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// User's roles.
        /// </summary>
        public IList<string> Roles { get; set; } = new List<string>();

        public IList<string> Permissions { get; set; } = new List<string>();

    }
}
