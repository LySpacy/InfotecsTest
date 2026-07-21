using InfotecsTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfotecsTest.Infrustraction.DataBase.DbContexts.Configurations;

internal class FileResultConfiguration : BaseConfiguration<FileResultEntity>
{
    public void Configure(EntityTypeBuilder<FileResultEntity> builder)
    {
        builder.ToTable("Results");

        builder.HasKey(result => result.Id);

        builder.Property(result => result.Id)
            .ValueGeneratedNever();

        builder.Property(result => result.FileId)
            .IsRequired();

        builder.Property(result => result.DeltaTime)
           .IsRequired();

        builder.Property(result => result.MinDate)
          .IsRequired();

        builder.Property(result => result.AverageExecutionTime)
          .IsRequired();

        builder.Property(result => result.AverageValue)
          .IsRequired();

        builder.Property(result => result.MedianValue)
          .IsRequired();

        builder.Property(result => result.MaxValue)
          .IsRequired();

        builder.Property(result => result.MinValue)
          .IsRequired();

        builder.HasIndex(value => value.FileId);

        builder.HasOne<FileEntity>()
            .WithMany()
            .HasForeignKey(value => value.FileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}