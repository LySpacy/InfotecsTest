using InfotecsTest.Application.Common.Helpers;
using InfotecsTest.Domain.Entities;

namespace InfotecsTest.Application.Interfaces.Helpers;

/// <summary>
/// Интерфейс парсера файла csv формата
/// </summary>
public interface ICsvParser
{
    /// <summary>
    /// Парсинг файла
    /// </summary>
    /// <param name="stream">Поток данных из файла</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Колеция из сущностей значений</returns>
    Task<IReadOnlyCollection<ParsedValue>> ParseAsync(
        Stream stream,
        CancellationToken cancellationToken);
}
