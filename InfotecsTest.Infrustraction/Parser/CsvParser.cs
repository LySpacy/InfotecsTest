using CsvHelper;
using CsvHelper.Configuration;
using InfotecsTest.Application.Common.Exceptions;
using InfotecsTest.Application.Common.Helpers;
using InfotecsTest.Application.Interfaces.Helpers;
using InfotecsTest.Domain.Entities;
using InfotecsTest.Infrustraction.Parser.Configurations;
using InfotecsTest.Infrustraction.Parser.Models;
using System.Globalization;

namespace InfotecsTest.Infrustraction.Parser;

/// <summary>
/// Парсер Csv файла
/// </summary>
public sealed class CsvParser : ICsvParser
{
    private const int MaxRows = 10_000;

    /// <summary>
    /// Асинхронный парсинг потока данных из csv файла
    /// </summary>
    /// <param name="stream">Поток данных</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список сущностей значений, полученный из потока данных</returns>
    /// <exception cref="InvalidCsvFileException">Ошибка при обработке данных из csv файла</exception>
    public async Task<IReadOnlyCollection<ParsedValue>> ParseAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(stream);

        using var csvReader = new CsvReader(
            reader, 
            new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            }
        );

        csvReader.Context.RegisterClassMap<CsvValueRecordMap>();

        var values = new List<ParsedValue>();

        try
        {
            await foreach (var record in csvReader.GetRecordsAsync<CsvValueRecord>(cancellationToken))
            {
                if (values.Count > MaxRows)
                {
                    throw new InvalidCsvFileException($"CSV файл не должен содержать более {MaxRows} строк");
                }

                var value = new ParsedValue(
                    record.Date,
                    record.ExecutionTime,
                    record.Value);

                values.Add(value);
            }
        }
        catch (ArgumentException exception)
        {
            throw new InvalidCsvFileException($"{exception.Message} | Строка {csvReader.Context.Parser.Row}", csvReader.Context.Parser.Row, exception);
        }
        catch (CsvHelperException exception)
        {
            throw new InvalidCsvFileException($"CSV файл содержит некорректный данные | Строка {csvReader.Context.Parser.Row} ", csvReader.Context.Parser.Row, exception);
        }

        if (values.Count == 0)
        {
            throw new InvalidCsvFileException($"CSV файл должен содержать хотя бы одну строку");
        }

        return values;
    }
}
