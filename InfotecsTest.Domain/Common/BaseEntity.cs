namespace InfotecsTest.Domain.Common;

/// <summary>
/// Базовая модель сущности, хранящейся в базе данных.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
