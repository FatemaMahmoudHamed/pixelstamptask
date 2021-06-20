using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStamp.Core.Entities;

namespace Moe.La.Infrastructure.EntityConfiguration
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons", "Content");

            builder.Property(m => m.Id)
                .ValueGeneratedNever();

            builder.Property(m => m.Description)
                .HasComment("The Description of the lesson")
                .HasMaxLength(2000)
                .IsRequired();

        }
    }
}
