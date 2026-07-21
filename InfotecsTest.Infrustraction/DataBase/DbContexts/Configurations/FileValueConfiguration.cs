using InfotecsTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfotecsTest.Infrustraction.DataBase.DbContexts.Configurations;

internal class FileValueConfiguration : BaseConfiguration<FileValueEntity>
{
    public void Configure(EntityTypeBuilder<FileValueEntity> builder)
    {
        builder.ToTable("Values");

        builder.HasKey(value => value.Id);

        builder.Property(value => value.Id)
            .ValueGeneratedNever();

        builder.Property(value => value.FileId)
            .IsRequired();

        builder.Property(value => value.Date)
           .IsRequired();
        
        builder.Property(value => value.ExecutionTime)
           .IsRequired();
        
        builder.Property(value => value.Value)
           .IsRequired();

        builder.HasIndex(value => value.FileId);

        builder.HasOne<FileEntity>()
            .WithMany()
            .HasForeignKey(value => value.FileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
