using InfotecsTest.Application.Interfaces.Repositories;
using InfotecsTest.Domain.Entities;
using InfotecsTest.Infrustraction.DataBase.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTest.Infrustraction.DataBase.Repositories;

public sealed class FileRepository : IFileRepository
{
    private readonly AppDbContext _context;

    public FileRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    public void Add(FileEntity file)
    {
        _context.Files.Add(file);
    }

    public Task<FileEntity?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _context.Files.FirstOrDefaultAsync(file => file.Name == name, cancellationToken);
                       
    }
}
