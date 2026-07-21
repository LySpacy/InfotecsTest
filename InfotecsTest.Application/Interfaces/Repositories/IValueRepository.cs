using InfotecsTest.Domain.Entities;

namespace InfotecsTest.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс для взаимодействия с таблицей значений
/// </summary>
public interface IValueRepository
{
    /// <summary>
    /// Фиксация удаления записей значений по идентификатору файла
    /// </summary>
    /// <param name="fileId">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task DeleteByFileIdAsync(Guid fileId, CancellationToken cancellationToken);

    /// <summary>
    /// Фиксация добавления значений в базу
    /// </summary>
    /// <param name="values">Список значений</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task AddRangeAsync(IEnumerable<FileValueEntity> values, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка из n элементов значений, отсортированный по начальному времени запуска, по идентификатору файла
    /// </summary>
    /// <param name="fileName">Идентификатор файла</param>
    /// <param name="count">Количество элементов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список из count элементов последних значений, отсортированный по начальному времени запуска</returns>
    Task<IEnumerable<FileValueEntity>> GetLastValueByFileIdAsync(Guid fileId, int count, CancellationToken cancellationToken);
}

