namespace InfotecsTest.Application.DTOs.Filters;

/// <summary>
/// Фильтр для таблицы результатов
/// </summary>
/// <param name="FileName">Имя файла</param>
/// <param name="TimeStartFirstOperationStart">Время первой операции (от)</param>
/// <param name="TimeStartFirstOperationEnd">Время первой операции (до)</param>
/// <param name="AverageValueeUp">Средний показатель (от)</param>
/// <param name="AverageValueeDown">Средний показатель (до)</param>
/// <param name="AverageExecutionTimeUp">Среднее время выполнения (от)</param>
/// <param name="AverageExecutionTimeDown">Среднее время выполнения (до)</param>
public record ResultFilter(
    string? FileName,
    DateTime? TimeStartFirstOperationStart,
    DateTime? TimeStartFirstOperationEnd,
    double? AverageValueeUp,
    double? AverageValueeDown,
    double? AverageExecutionTimeUp,
    double? AverageExecutionTimeDown);
