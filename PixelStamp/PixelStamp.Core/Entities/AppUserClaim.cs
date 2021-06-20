using Microsoft.AspNetCore.Identity;
using System;

namespace PixelStamp.Core.Entities
{
    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        public virtual AppUser User { get; set; }
    }
}
