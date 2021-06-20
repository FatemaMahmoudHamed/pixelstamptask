using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PixelStamp.Core.Entities;
using System;

namespace PixelStamp.Infrastructure.DbContexts
{
    public class QueryDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {

        public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
        {
        }
    }
}
