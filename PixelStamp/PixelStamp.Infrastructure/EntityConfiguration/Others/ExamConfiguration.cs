using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelStamp.Core.Entities;

namespace Moe.La.Infrastructure.EntityConfiguration
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exams", "Content");

            builder.Property(m => m.Id)
                .ValueGeneratedNever();
        }
    }
}
