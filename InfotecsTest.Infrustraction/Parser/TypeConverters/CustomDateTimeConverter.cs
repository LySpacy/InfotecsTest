using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

public sealed class CustomDateTimeConverter : DefaultTypeConverter
{
    private const string Format = "yyyy-MM-dd'T'HH-mm-ss.ffff'Z'";

    public override object ConvertFromString(
        string? text,
        IReaderRow row,
        MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new TypeConverterException(this, memberMapData, text, row.Context, "Дата отсутствует.");

        if (DateTime.TryParseExact(
                text,
                Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out var result))
        {
            return result;
        }

        throw new TypeConverterException(
            this,
            memberMapData,
            text,
            row.Context,
            $"Некорректный формат даты: {text}");
    }
}