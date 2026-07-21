namespace InfotecsTest.Application.Common.Exceptions;

/// <summary>
/// Исключение о невалидности csv файла
/// </summary>
public class InvalidCsvFileException : Exception
{
    public int? RowNumber { get; }
    public InvalidCsvFileException(
        string message,
        int? rowNumber = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        RowNumber = rowNumber;
    }
}
