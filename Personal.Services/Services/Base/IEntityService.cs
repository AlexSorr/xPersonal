using System.Linq.Expressions;

using Personal.Models.Model.Base;

namespace Personal.Services.Base;

/// <summary>
/// Интерфейс, определяющий методы для работы с сущностями в базе данных.
/// </summary>
/// <typeparam name="T">Тип сущности, с которой будет работать сервис. Должен реализовывать интерфейс <see cref="IEntity"/>.</typeparam>
public interface IEntityService<T> where T : IEntity {

    /// <summary>
    /// Загрузить сущность
    /// </summary>
    public T LoadById(Guid id);

    public Task<T> LoadByIdAsync(Guid id);

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    public Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate); 

    
    // TODO рассмотреть вариант с сохранением конкретно одной сущности в БД

    /// <summary>
    /// Сохранить сущность
    /// </summary>
    public void Save(T entity);

    public Task SaveAsync(T entity);

    public Task SaveBatchAsync(IEnumerable<T> batch);

    public Task SaveBatchAsync(IEnumerable<T> batch, int batchSize);



    /// <summary>
    /// Удалить объект
    /// </summary>
    public void Delete(T entity);

    public Task DeleteAsync(T entity);

    public void DeleteRange(IEnumerable<T> entities);

    public Task DeleteRangeAsync(IEnumerable<T> entities);




    /// <summary>
    /// Объект существует
    /// </summary>
    public bool EntityExists(Expression<Func<T, bool>> expression);

    public Task<bool> EntityExistsAsync(Expression<Func<T, bool>> expression);

    public bool EntityExists(Expression<Func<T, bool>> expression, out Guid id);

}
