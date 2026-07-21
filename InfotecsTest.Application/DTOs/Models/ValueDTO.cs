namespace InfotecsTest.Application.DTOs.Models;

/// <summary>
/// Значение
/// </summary>
/// <param name="FileId">Идентификатор файла, источника значения</param>
/// <param name="Date">Время начала</param>
/// <param name="ExecutionTime">Время выполнения в секундах</param>
/// <param name="Value">Показатель</param>
public record ValueDTO(
    Guid FileId,
    DateTime Date,
    double ExecutionTime,
    double Value
    );
