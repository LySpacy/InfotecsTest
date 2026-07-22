using InfotecsTest.Application.DTOs.Filters;
using InfotecsTest.Application.DTOs.Models;
using InfotecsTest.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsTest.API.Controllers;

[ApiController]
[Route("api/timescale")]
public sealed class TimescaleController : Controller
{
    private readonly ITimescaleService _timescaleService;

    /// <summary>
    /// Конструктор контроллера.
    /// </summary>
    /// <param name="timescaleService">Сервис для работы с данными временных рамок</param>
    public TimescaleController(ITimescaleService timescaleService)
    {
        _timescaleService = timescaleService;
    }

    /// <summary>
    /// Метод загрузки данных через файл csv формата
    /// </summary>
    /// <param name="file">Файл</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Возвращает статус выполнения операции</returns>
    [HttpPost("loadCSV")]
    public async Task<IActionResult> LoadCSV(
    IFormFile file,
    CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            return BadRequest("Файл пуст");

        if (!Path.GetExtension(file.FileName)
            .Equals(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Поддерживаются только файлы с расширением .csv");
        }

        await using var stream = file.OpenReadStream();

        await _timescaleService.LoadCSVAsync(
            file.FileName,
            stream,
            cancellationToken);

        return Ok("Данные загружены.");
    }

    /// <summary>
    /// Метод получения списка результатов, с возможность фильтрации
    /// </summary>
    /// <param name="filter">Фильтр</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список результатов, соотвесвуюзий фильтру</returns>
    [HttpGet("results")]
    public async Task<IReadOnlyCollection<ResultDTO>> GetResults([FromQuery] ResultFilter? filter, CancellationToken cancellationToken)
    {
        var results = await _timescaleService.GetResultsByFilterAsync(filter, cancellationToken);

        return results;
    }

    /// <summary>
    /// получения списка последних 10 значений, отсортированных по начальному времени запуска Date по имени заданного файла
    /// </summary>
    /// <param name="fileName">Имя файла</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список из 10 значение по заданому имени файла</returns>
    [HttpGet("lastValueByFile")]
    public async Task<IReadOnlyCollection<ValueDTO>> GetLastValueByFileName(string fileName, CancellationToken cancellationToken)
    {
        var values = await _timescaleService.GetLastValuesByFileNameAsync(fileName, cancellationToken);

        return values;
    }
}
