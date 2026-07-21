using InfotecsTest.Domain.Common;

namespace InfotecsTest.Domain.Entities;

/// <summary>
/// Сущность файла
/// </summary>
public sealed class FileEntity : BaseEntity
{
    /// <summary>
    /// Имя файла
    /// </summary>
    public string Name { get; private set; }

    //Прочие параметры файла...

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name">Имя файла</param>
    public FileEntity(string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("Имя файла не может пустым");
        }

        Name = name;
    }
}
