using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStamp.Core.Entities;

namespace Moe.La.Infrastructure.EntityConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses", "Content");

            builder.Property(m => m.Id)
                .ValueGeneratedNever();

            builder.Property(m => m.Name)
                .HasComment("The name of the course")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
