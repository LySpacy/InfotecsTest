using InfotecsTest.Application.DTOs.Filters;
using InfotecsTest.Application.Interfaces.Repositories;
using InfotecsTest.Domain.Entities;
using InfotecsTest.Infrustraction.DataBase.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTest.Infrustraction.DataBase.Repositories;

public sealed class FileResultRepository : IResultRepository
{
    private readonly AppDbContext _context;

    public FileResultRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public async Task AddAsync(FileResultEntity result, CancellationToken cancellationToken)
    {
        await _context.Results.AddAsync(result);
    }

    public Task DeleteByFileIdAsync(Guid fileId, CancellationToken cancellationToken)
    {
        return _context.Results
                    .Where(result => result.FileId == fileId)
                    .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<FileResultEntity>> GetByFilterAsync(ResultFilter? filter, CancellationToken cancellationToken)
    {
        var query = _context.Results.AsQueryable();

        if (!string.IsNullOrEmpty(filter.FileName))
        {
            var file = _context.Files.FirstOrDefault(f => f.Name == filter.FileName);

            if (file is not null)
            {
                query = query.Where(r => r.FileId == file.Id);
            }

            return new List<FileResultEntity>();
            
        }

        if (filter.AverageValueeDown.HasValue)
        {
            query = query.Where(r =>
                r.AverageValue >= filter.AverageValueeDown.Value);
        }

        if (filter.AverageValueeUp.HasValue)
        {
            query = query.Where(r =>
                r.AverageValue <= filter.AverageValueeUp.Value);
        }

        if (filter.AverageExecutionTimeDown.HasValue)
        {
            query = query.Where(r =>
                r.AverageExecutionTime >=
                filter.AverageExecutionTimeDown.Value);
        }

        if (filter.AverageExecutionTimeUp.HasValue)
        {
            query = query.Where(r =>
                r.AverageExecutionTime <=
                filter.AverageExecutionTimeUp.Value);
        }

        if (filter.TimeStartFirstOperationStart.HasValue)
        {
            query = query.Where(r =>
                r.MinDate >= filter.TimeStartFirstOperationStart.Value);
        }

        if (filter.TimeStartFirstOperationEnd.HasValue)
        {
            query = query.Where(r =>
                r.MinDate <= filter.TimeStartFirstOperationEnd.Value);
        }

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
