using InfotecsTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfotecsTest.Infrustraction.DataBase.DbContexts.Configurations;

internal class FileConfiguration : BaseConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.ToTable("Files");

        builder.HasKey(file => file.Id);

        builder.Property(file => file.Id)
            .ValueGeneratedNever();

        builder.Property(file => file.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(file => file.Name);
    }
}
