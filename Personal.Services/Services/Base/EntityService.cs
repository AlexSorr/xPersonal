using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage;


using Personal.Models.Data;
using Personal.Models.Model.Base;


namespace Personal.Services.Base;

/// <summary>
/// Сервис для работы с сущностями типа <typeparamref name="T"/> в базе данных.
/// Предоставляет методы для добавления, удаления, обновления и получения сущностей.
/// </summary>
/// <typeparam name="T">Тип сущности, которая будет обрабатываться сервисом.</typeparam>
public class EntityService<T> : IEntityService<T> where T : class, IEntity {
    #region properties

    protected readonly ApplicationDbContext _context;

    protected readonly ILogger<EntityService<T>> _logger;

    private readonly IEntityServiceFactory _serviceFactory;
    
    public EntityService(ApplicationDbContext context, ILogger<EntityService<T>> logger, IEntityServiceFactory serviceFactory) { 
        _context = context; 
        _logger = logger; 
        _serviceFactory = serviceFactory;
    }

    /// <summary>
    /// Коллекция сущностей <typeparamref name="T"/> в БД
    /// </summary>
    protected DbSet<T> EntitySet => _context.Set<T>();

    /// <summary>
    /// Включить жадную загрузку в запрос для поиска сущностей
    /// </summary>
    /// <param name="query">Множество искомых объектов</param>
    /// <param name="includedProperties">Свойства, которые нужно загрузить из БД</param>
    /// <returns></returns>
    protected IQueryable<T> GetEagerLoadingQuery(IEnumerable<T> entities, params Expression<Func<T, object>>[] includedProperties) {
        var result = entities.AsQueryable();
        foreach (var prop in includedProperties) 
            result = result.Include(prop);
        return result;
    }

    #endregion

    #region InterfacesImplementation

    /// <summary>
    /// Загрузка сущностей
    /// </summary>

    /// <inheritdoc/>
    public T LoadById(Guid id) => EntitySet.FirstOrDefault(entity => entity.Id == id);

    public async Task<T> LoadByIdAsync(Guid id) => await EntitySet.FirstOrDefaultAsync(entity => entity.Id == id);

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => EntitySet.Where(predicate);

    public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate) => await EntitySet.Where(predicate).ToListAsync();





    /// <summary>
    /// Сохранение сущностей
    /// </summary>
    public void Save(T entity) {
        if (entity == null)
            return;

        var entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached) {
            entity.CreationDate = DateTime.UtcNow;
            EntitySet.Add(entity);
        } 
        else {
            entity.ChangeDate = DateTime.UtcNow;
            entry.State = EntityState.Modified;
        }
        _context.SaveChanges();
    }

    public async Task SaveAsync(T entity) {
        var entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached) {
            entity.CreationDate = DateTime.UtcNow;
            await EntitySet.AddAsync(entity);
        } else {
            entity.ChangeDate = DateTime.UtcNow;
            entry.State = EntityState.Modified;
        }
        await _context.SaveChangesAsync();
    }

    // TODO Сделать в конфиге
    private const int defaultSavingBatchSize = 500;
    public async Task SaveBatchAsync(IEnumerable<T> entities) => await SaveBatchAsync(entities, defaultSavingBatchSize);

    // FIXME Сделать также через Entry, в текущей реализации будет дублировать
    public async Task SaveBatchAsync(IEnumerable<T> entities, int batchSize = defaultSavingBatchSize) {
        if (entities == null || !entities.Any()) return;
        
        using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync()) {
            IList<T> package = entities as IList<T> ?? entities.ToList();
            int packageCount = package.Count();
            try {
                T[] updatedObjects;
                for (int currentIndex = 0; currentIndex < packageCount; currentIndex += batchSize) {
                    IEnumerable<T> batch = package.Skip(currentIndex).Take(batchSize);
                    
                    //новые объекты добавляем в конткст
                    updatedObjects = batch.Where(x => GetEntityState(x) == EntityState.Detached).ToArray();
                    if (updatedObjects.Any())
                        await _context.AddRangeAsync(batch);  

                    await _context.SaveChangesAsync();  
                } 
                await transaction.CommitAsync();
            } catch {
                await transaction.RollbackAsync();
                throw;
            } finally {
                _context.ChangeTracker.Clear();
            }
        }
    }




    /// <summary>
    /// Удалить объект
    /// </summary>
    public void Delete(T entity) {
        if (!IsTrackedByContext(entity))
            return;
        _context.Remove(entity);
        try { _context.SaveChanges(); } catch { throw; }
    }

    public async Task DeleteAsync(T entity) {
        if (!IsTrackedByContext(entity))
            return;
        EntitySet.Remove(entity);
        try { await _context.SaveChangesAsync(); } catch { throw; }
    }

    public void DeleteRange(IEnumerable<T> entities) {
        entities = entities.Where(IsTrackedByContext).ToArray();
        EntitySet.RemoveRange(entities);
        try { _context.SaveChanges(); } catch { throw; }
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entityList) {
        entityList = entityList.Where(IsTrackedByContext).ToArray();
        if (!entityList.Any()) return;
        EntitySet.RemoveRange(entityList);
        try { await _context.SaveChangesAsync(); } catch { throw; }
    }




    /// <summary>
    /// Проверка сущестования объекта
    /// </summary>
    public bool EntityExists(Expression<Func<T, bool>> expression) => EntitySet.Any(expression);

    public Task<bool> EntityExistsAsync(Expression<Func<T, bool>> expression) => EntitySet.AnyAsync(expression);

    public bool EntityExists(Expression<Func<T, bool>> expression, out Guid id) {
        var result = EntitySet.FirstOrDefault(expression);
        id = result?.Id ?? Guid.Empty;
        return result != null;
    }


    #endregion

    #region ServiceMethods

    /// <summary>
    /// Получить состояние объекта в конткетсе БД
    /// </summary>
    protected EntityState GetEntityState(T entity) {
        if (entity == null)
            return EntityState.Detached;
        return _context.Entry(entity)?.State ?? EntityState.Detached;
    }

    /// <summary>
    /// Сущность отслеживается контекстом БД
    /// </summary>
    private bool IsTrackedByContext(T entity) => _context.ChangeTracker.Entries().Any(x => x.Entity == x);

    #endregion

}