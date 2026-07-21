using InfotecsTest.Application.DTOs.Filters;
using InfotecsTest.Domain.Entities;

namespace InfotecsTest.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс для взаимодействия с таблицей результатов
/// </summary>
public interface IResultRepository
{
    /// <summary>
    /// Получение списка результатов из базы с фильтрацией
    /// </summary>
    /// <param name="filter">Фильтр</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    Task<IEnumerable<FileResultEntity>> GetByFilterAsync(ResultFilter? filter, CancellationToken cancellationToken);

    /// <summary>
    /// Фиксация добавления результата в таблицу для DbContext
    /// </summary>
    /// <param name="result"></param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task AddAsync(FileResultEntity result, CancellationToken cancellationToken);

    /// <summary>
    /// Фиксация удаления результата по идентификатору файла
    /// </summary>
    /// <param name="fileId">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task DeleteByFileIdAsync(Guid fileId, CancellationToken cancellationToken);

}

