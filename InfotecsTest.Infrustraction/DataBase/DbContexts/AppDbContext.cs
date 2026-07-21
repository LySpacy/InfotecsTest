using InfotecsTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTest.Infrustraction.DataBase.DbContexts;

public sealed class AppDbContext : BaseDbContext
{
    public DbSet<FileEntity> Files => Set<FileEntity>();

    public DbSet<FileValueEntity> Values => Set<FileValueEntity>();

    public DbSet<FileResultEntity> Results => Set<FileResultEntity>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
