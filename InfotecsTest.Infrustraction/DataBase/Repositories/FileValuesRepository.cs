using InfotecsTest.Application.Interfaces.Repositories;
using InfotecsTest.Domain.Entities;
using InfotecsTest.Infrustraction.DataBase.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTest.Infrustraction.DataBase.Repositories;

public sealed class FileValuesRepository : IValueRepository
{
    private readonly AppDbContext _context;

    public FileValuesRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public async Task AddRangeAsync(IEnumerable<FileValueEntity> values, CancellationToken cancellationToken)
    {
        await _context.Values.AddRangeAsync(values, cancellationToken);
    }
    public Task DeleteByFileIdAsync(Guid fileId, CancellationToken cancellationToken)
    {
        return _context.Values
                    .Where(result => result.FileId == fileId)
                    .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<FileValueEntity>> GetLastValueByFileIdAsync(Guid fileId, int count = 10, CancellationToken cancellationToken = default)
    {
        var query = _context.Values.AsQueryable();

        query = query.Where(v => v.FileId == fileId)
                    .OrderBy(v => v.Date);

        return await query
            .AsNoTracking()
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}
