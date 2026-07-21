namespace InfotecsTest.Application.Common.Helpers;

public sealed class ParsedValue
{
    /// <summary>
    /// Время начала
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// Время выполнения в секундах
    /// </summary>
    public double ExecutionTime { get; private set; }

    /// <summary>
    /// Показатель в виде числа с плавающей запятой
    /// </summary>
    public double Value { get; private set; }

    public ParsedValue(DateTime date, double executionTime, double value)
    {
        if(date.Date > DateTime.UtcNow.Date)
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
