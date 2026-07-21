using InfotecsTest.Domain.Common;

namespace InfotecsTest.Domain.Entities;

/// <summary>
/// Интегральные результаты
/// </summary>
public sealed class FileResultEntity : BaseEntity
{
    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public Guid FileId { get; private set; }

    /// <summary>
    /// Дельта времени Date в секундах
    /// </summary>
    public double DeltaTime { get; private set; }

    /// <summary>
    /// Минимальное дата и время, как момент запуска первой операции
    /// </summary>
    public DateTime MinDate { get; private set; }

    /// <summary>
    /// Среднее время выполнения
    /// </summary>
    public double AverageExecutionTime { get; private set; }

    /// <summary>
    /// Среднее значение по показателям
    /// </summary>
    public double AverageValue { get; private set; }

    /// <summary>
    /// Медиана по показателям
    /// </summary>
    public double MedianValue { get; private set; }

    /// <summary>
    /// Максимальное значение показателя
    /// </summary>
    public double MaxValue { get; private set; }

    /// <summary>
    /// Минимальное значение показателя
    /// </summary>
    public double MinValue { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    private FileResultEntity()
    { }

    /// <summary>
    /// Подсчет результата по списку значений
    /// </summary>
    /// <param name="fileId">Идентификатор файла</param>
    /// <param name="values">Массив значений</param>
    /// <returns>Сущность результата</returns>
    public static FileResultEntity Calculate(Guid fileId, IReadOnlyCollection<FileValueEntity> values)
    {
        if (fileId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор файла не может быть пустым");
        }

        if (values.Count == 0)
        {
            throw new ArgumentException("Массив значений не должен быть пустым");
        }

        // Получаем массив значений
        var orderedValues = values
            .Select(v => v.Value)
            .OrderBy(v => v)
            .ToArray();

        // Подсчитываем медиану значений
        var median = CalculateMedian(orderedValues);

        return new FileResultEntity()
        {
            FileId = fileId,
            DeltaTime = (values.Max(v => v.Date) - values.Min(v => v.Date)).TotalSeconds,
            MinDate = values.Min(v => v.Date),
            AverageExecutionTime = values.Average(v => v.ExecutionTime),
            AverageValue = values.Average(v => v.Value),
            MedianValue = median,
            MaxValue = values.Max(v => v.Value),
            MinValue = values.Min(v => v.Value)
        };
    }

    /// <summary>
    /// Подсчет медианы
    /// </summary>
    /// <param name="orderedValues">Массив данных</param>
    /// <returns>Медиана массива</returns>
    private static double CalculateMedian(double[] orderedValues)
    {
        var middle = orderedValues.Length / 2;

        return orderedValues.Length % 2 == 0 ? (orderedValues[middle - 1] + orderedValues[middle]) / 2 : orderedValues[middle];
    }
}
