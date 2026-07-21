using InfotecsTest.Application.Interfaces.Repositories;
using InfotecsTest.Infrustraction.DataBase.DbContexts;

namespace InfotecsTest.Infrustraction.DataBase.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public Task SaveChangeAsync(CancellationToken cancellationToken)
    {
       return _context.SaveChangesAsync(cancellationToken);
    }
}