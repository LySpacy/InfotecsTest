using InfotecsTest.Domain.Common;

namespace InfotecsTest.Domain.Entities;

/// <summary>
/// Сущность значения
/// </summary>
public sealed class FileValueEntity : BaseEntity
{
    /// <summary>
    /// Ссылка на файл, из которого берется значение
    /// </summary>
    public Guid FileId { get; private set; }

    /// <summary>
    /// Время начала
    /// </summary>
    public DateTime Date {  get; private set; }

    /// <summary>
    /// Время выполнения в секундах
    /// </summary>
    public double ExecutionTime { get; private set; }

    /// <summary>
    /// Показатель в виде числа с плавающей запятой
    /// </summary>
    public double Value { get; private set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="fileId">Идентификатор файла</param>
    /// <param name="date">Время начала</param>
    /// <param name="executionTime">Время выполнения в секундах</param>
    /// <param name="value">Показатель в виде числа с плавающей запятой</param>
    /// <exception cref="ArgumentException">Ошибка значения присылаемого параметра</exception>
    public FileValueEntity(Guid fileId, DateTime date, double executionTime, double value)
    {
        if (fileId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор файла не может быть пустым");
        }

        FileId = fileId;

        if (date.Date > DateTime.UtcNow.Date)
        {
            throw new ArgumentException($"Дата не может быть позже текущей | {date}");
        }

        if (date.Date < new DateTime(2000, 1, 1).Date)
        {
            throw new ArgumentException($"Дата не может быть раньше 01.01.2000 | {date}");
        }

        Date = date;

        if (executionTime < 0)
        {
            throw new ArgumentException($"Время выполнения не может быть меньше 0 | {executionTime}");
        }

        ExecutionTime = executionTime;

        if (value < 0)
        {
            throw new ArgumentException($"Значение показателья не может быть меньше 0 | {value}");
        }

        Value = value;
    }
}
