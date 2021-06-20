using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStamp.Core.Entities;

namespace Moe.La.Infrastructure.EntityConfiguration
{
    class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users", "Application");

            builder.Property(m => m.CreatedOn)
               .HasComment("The creation datetime")
               .IsRequired();
           
        }
    }
}
