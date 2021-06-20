using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStamp.Core.Entities;

namespace Moe.La.Infrastructure.EntityConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions", "Content");

            builder.Property(m => m.Id)
                .ValueGeneratedNever();

            builder.Property(m => m.Text)
                .HasComment("The Description of the lesson")
                .HasMaxLength(2000)
                .IsRequired();

        }
    }
}
