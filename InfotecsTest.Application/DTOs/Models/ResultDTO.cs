namespace InfotecsTest.Application.DTOs.Models;

/// <summary>
/// Результат
/// </summary>
/// <param name="FileId">Идентификатор файла, которому принадлежит результат</param>
/// <param name="DeltaTime">Дельта времени Date в секундах</param>
/// <param name="StartDate">Момент запуска первой операции</param>
/// <param name="AverageExecutionTime">Среднее время выполнения</param>
/// <param name="AverageValue">Среднее значение по показателям</param>
/// <param name="MedianValue">Медиана по показателям</param>
/// <param name="MaxValue">Максимальное значение показателя</param>
/// <param name="MinValue">Минимальное значение показателя</param>
public record ResultDTO(
    Guid FileId,
    double DeltaTime,
    DateTime StartDate,
    double AverageExecutionTime,
    double AverageValue,
    double MedianValue,
    double MaxValue,
    double MinValue);
