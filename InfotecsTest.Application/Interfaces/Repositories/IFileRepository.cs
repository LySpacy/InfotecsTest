using InfotecsTest.Domain.Entities;

namespace InfotecsTest.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс для взаимодействия с таблицей файлов
/// </summary>
public interface IFileRepository
{
    /// <summary>
    /// Получения сущности файла по имени, если он есть в базе
    /// </summary>
    /// <param name="name">Имя файла</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Сущность файла, если он есть в базе или null</returns>
    Task<FileEntity?> GetByNameAsync(string name, CancellationToken cancellationToken);
    
    /// <summary>
    /// Фиксация добавления файла в таблицу для DbContext
    /// </summary>
    /// <param name="file">Сущность файла</param>
    void Add(FileEntity file);
}

