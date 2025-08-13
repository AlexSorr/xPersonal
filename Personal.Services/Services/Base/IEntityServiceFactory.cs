using IEntity = Personal.Models.Model.Base.IEntity;

namespace Personal.Services.Base;

/// <summary>
/// Фабрика для создания экземпляров сервисов, реализующих <see cref="IEntityService{T}"/>.
/// </summary>
public interface IEntityServiceFactory {
    
    /// <summary>
    /// Создает экземпляр <see cref="IEntityService{T}"/> для заданного типа сущности.
    /// </summary>
    public IEntityService<T> Create<T>() where T : class, IEntity;
    
}
