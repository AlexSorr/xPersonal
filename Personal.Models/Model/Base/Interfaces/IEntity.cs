namespace Personal.Models.Model.Base;

/// <summary>
/// Интерфейс, который должны реализовывать все  хранимые в БД сущности в приложении.
/// </summary>
public interface IEntity {

    /// <summary>
    /// Идентификатор сущности.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Дата создания сущности.
    /// </summary>
    DateTime CreationDate { get; set; }

    /// <summary>
    /// Дата последнего изменения сущности. Может быть <c>null</c>, если сущность еще не была изменена.
    /// </summary>
    DateTime? ChangeDate { get; set; }

}
