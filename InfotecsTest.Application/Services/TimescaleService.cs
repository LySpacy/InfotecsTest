using InfotecsTest.Application.DTOs.Filters;
using InfotecsTest.Application.DTOs.Models;
using InfotecsTest.Application.Interfaces.Helpers;
using InfotecsTest.Application.Interfaces.Repositories;
using InfotecsTest.Application.Interfaces.Services;
using InfotecsTest.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InfotecsTest.Application.Services;

public sealed class TimescaleService : ITimescaleService
{
    private const int LastValuesCount = 10;

    private readonly IFileRepository _fileRepository;
    private readonly IValueRepository _valueRepository;
    private readonly IResultRepository _resultRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICsvParser _csvParser;

    public TimescaleService(
        IFileRepository fileRepository,
        IValueRepository valueRepository,
        IResultRepository resultRepository,
        IUnitOfWork unitOfWork,
        ICsvParser csvParser)
    {
        _fileRepository = fileRepository;
        _valueRepository = valueRepository;
        _resultRepository = resultRepository;
        _unitOfWork = unitOfWork;
        _csvParser = csvParser;
    }

    public async Task<List<ValueDTO>> GetLastValuesByFileNameAsync(
        string fileName,
        CancellationToken cancellationToken)
    {
        var file = await _fileRepository.GetByNameAsync(
            fileName,
            cancellationToken);

        if (file is null)
        {
            throw new FileNotFoundException(
                $"Файс с именем '{fileName}'не найден.");
        }

        var values = await _valueRepository.GetLastValueByFileIdAsync(
            file.Id,
            LastValuesCount,
            cancellationToken);

        return values
            .Select(x => new ValueDTO(
                x.FileId,
                x.Date,
                x.ExecutionTime,
                x.Value
            ))
            .ToList();
    }

    public async Task<List<ResultDTO>> GetResultsByFilterAsync(
        ResultFilter filter,
        CancellationToken cancellationToken)
    {
        var results = await _resultRepository.GetByFilterAsync(
            filter,
            cancellationToken);

        return results
            .Select(x => new ResultDTO(
                x.FileId,
                x.DeltaTime,
                x.MinDate,
                x.AverageExecutionTime,
                x.AverageValue,
                x.MedianValue,
                x.MaxValue,
                x.MinValue))
            .ToList();
    }

    public async Task LoadCSVAsync(
        string fileName,
        Stream csvFileStream,
        CancellationToken cancellationToken)
    {
        var parsedValues = await _csvParser.ParseAsync(
            csvFileStream,
            cancellationToken);

        var fileEntity = await _fileRepository.GetByNameAsync(
            fileName,
            cancellationToken);

        if (fileEntity is null)
        {
            fileEntity = new FileEntity(
                fileName);

            _fileRepository.Add(fileEntity);
        }

        var values = parsedValues
            .Select(x => new FileValueEntity(
                fileEntity.Id,
                x.Date,
                x.ExecutionTime,
                x.Value))
            .ToList();


        var result = FileResultEntity.Calculate(
            fileEntity.Id,
            values);

        await _valueRepository.DeleteByFileIdAsync(
                    fileEntity.Id,
                    cancellationToken);

        await _resultRepository.DeleteByFileIdAsync(
            fileEntity.Id,
            cancellationToken);

        await _valueRepository.AddRangeAsync(values, cancellationToken);
        await _resultRepository.AddAsync(result, cancellationToken);

        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}