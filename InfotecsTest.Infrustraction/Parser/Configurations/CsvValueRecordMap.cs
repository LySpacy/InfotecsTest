using CsvHelper.Configuration;
using InfotecsTest.Infrustraction.Parser.Models;

namespace InfotecsTest.Infrustraction.Parser.Configurations;

internal sealed class CsvValueRecordMap : ClassMap<CsvValueRecord>
{
    public CsvValueRecordMap()
    {
        Map(record => record.Date)
            .Name("Date")
            .TypeConverter<CustomDateTimeConverter>();

        Map(record => record.ExecutionTime)
            .Name("ExecutionTime");

        Map(record => record.Value)
            .Name("Value");
    }
}