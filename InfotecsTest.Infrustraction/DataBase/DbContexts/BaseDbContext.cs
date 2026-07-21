using InfotecsTest.Domain.Entities;
using InfotecsTest.Infrustraction.DataBase.DbContexts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTest.Infrustraction.DataBase.DbContexts;

public abstract partial class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
    }
}
