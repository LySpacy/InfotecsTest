using InfotecsTest.Application.DTOs.Filters;
using InfotecsTest.Application.DTOs.Models;
using Microsoft.AspNetCore.Http;

namespace InfotecsTest.Application.Interfaces.Services;

public interface ITimescaleService
{
    Task LoadCSVAsync(string fileName, Stream csvFileStream, CancellationToken cancellationToken);
    Task<List<ResultDTO>> GetResultsByFilterAsync(ResultFilter filter, CancellationToken cancellationToken);
    Task<List<ValueDTO>> GetLastValuesByFileNameAsync(string fileName, CancellationToken cancellationToken);
}
