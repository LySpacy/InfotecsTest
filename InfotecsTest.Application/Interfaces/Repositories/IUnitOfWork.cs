namespace InfotecsTest.Application.Interfaces.Repositories;

/// <summary>
/// Интерфейс для взаимодействия с базой данных
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохранение всех фиксаций DbContext'а в базу данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SaveChangeAsync(CancellationToken cancellationToken);
}

