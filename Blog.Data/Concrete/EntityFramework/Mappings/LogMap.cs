using Blog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Concrete.EntityFramework.Mappings
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id); // primary key
            builder.Property(l => l.Id).ValueGeneratedOnAdd(); // 1,1 artma

            builder.Property(l => l.MachineName).IsRequired(); // NOT NULLABLE
            builder.Property(l => l.MachineName).HasMaxLength(50); // NVARCHAR(50)

            builder.Property(l => l.Logged).IsRequired();

            builder.Property(l => l.Level).IsRequired();
            builder.Property(l => l.Level).HasMaxLength(50);

            builder.Property(l => l.Message).IsRequired();
            builder.Property(l => l.Message).HasColumnType("NVARCHAR(MAX)");

            builder.Property(l => l.Logger).IsRequired(false);
            builder.Property(l => l.Logger).HasMaxLength(250);

            builder.Property(l => l.Callsite).IsRequired(false);
            builder.Property(l => l.Callsite).HasColumnType("NVARCHAR(MAX)");

            builder.Property(l => l.Exception).IsRequired(false);
            builder.Property(l => l.Exception).HasColumnType("NVARCHAR(MAX)");

            builder.ToTable("Logs");
        }
    }
}