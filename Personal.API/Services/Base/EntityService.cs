﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

using Personal.Data;
using Personal.Model.Base;

namespace Personal.API.Services.Base;

/// <summary>
/// Сервис для работы с сущностями типа <typeparamref name="T"/> в базе данных.
/// Предоставляет методы для добавления, удаления, обновления и получения сущностей.
/// </summary>
/// <typeparam name="T">Тип сущности, которая будет обрабатываться сервисом.</typeparam>
public class EntityService<T> : IEntityService<T> where T : class, IEntity {
    #region properties

    /// <summary>
    /// Контекст базы данных, используемый для взаимодействия с данными.
    /// </summary>
    protected readonly ApplicationDbContext _context;

    /// <summary>
    /// Набор сущностей типа <typeparamref name="T"/>
    /// </summary>
    protected DbSet<T> EntitySet => _context.Set<T>();

    /// <summary>
    /// Запрос сущностей типа <typeparamref name="T"/> с применением жадной загрузки 
    /// связанных данных.
    /// </summary>
    protected IQueryable<T> PreloadedEntityQuery => ApplyEagerLoading(EntitySet);

    /// <summary>
    /// Логгер для записи событий и ошибок, связанных с операциями в сервисе.
    /// </summary>
    protected readonly ILogger<EntityService<T>> _logger;

    /// <summary>
    /// Фабрика для создания экземпляров сервисов работы с сущностями.
    /// </summary>
    private readonly IEntityServiceFactory _serviceFactory;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EntityService{T}"/> с указанными параметрами.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <param name="logger">Логгер для записи событий и ошибок.</param>
    /// <param name="serviceFactory">Фабрика для создания экземпляров сервисов работы с сущностями.</param>
    public EntityService(ApplicationDbContext context, ILogger<EntityService<T>> logger, IEntityServiceFactory serviceFactory) { 
        _context = context; 
        _logger = logger; 
        _serviceFactory = serviceFactory;
    }

    /// <summary>
    /// Получает экземпляр <see cref="IEntityService{T1}"/> для указанного типа сущности с использованием фабрики сервисов.
    /// </summary>
    /// <typeparam name="T1">Тип сущности, для которого необходимо получить сервис. Должен реализовывать <see cref="IEntity"/>.</typeparam>
    /// <returns>Экземпляр <see cref="IEntityService{T1}"/> для работы с указанным типом сущности.</returns>
    public IEntityService<T1> GetEntityService<T1>() where T1 : class, IEntity => _serviceFactory.Create<T1>();

    #endregion

    #region InterfacesImplementation

    /// <inheritdoc/>
    public IEnumerable<T> All() => PreloadedEntityQuery;

    #pragma warning disable CS8603

    /// <inheritdoc/>
    public T LoadById(long id) => PreloadedEntityQuery.FirstOrDefault(entity => entity.Id == id);

    /// <inheritdoc/>
    public async Task<T> LoadByIdAsync(long id) => await PreloadedEntityQuery.FirstOrDefaultAsync(entity => entity.Id == id);

    #pragma warning restore CS8603

    /// <inheritdoc/>
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) => await PreloadedEntityQuery.Where(predicate).ToListAsync();

    /// <inheritdoc/>
    public async Task SaveAsync(T entity) {
        EntityEntry<T> entry = _context.Entry(entity);
        if (entry.State == EntityState.Detached) {
            entity.CreationDate = DateTime.UtcNow;
            await EntitySet.AddAsync(entity);
        } else {
            entity.ChangeDate = DateTime.UtcNow;
            entry.State = EntityState.Modified;
        }
        await _context.SaveChangesAsync();
    }

    private const int defaultSavingBatchSize = 1000;
    /// <inheritdoc/>
    public async Task SaveBatchAsync(IEnumerable<T> entities) => await SaveBatchAsync(entities, defaultSavingBatchSize);

    /// <inheritdoc/>
    public async Task SaveBatchAsync(IEnumerable<T> entities, int batchSize = defaultSavingBatchSize) {
        if (entities == null || !entities.Any()) return;
        
        using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync()) {
            IList<T> package = entities as IList<T> ?? entities.ToList();
            int packageCount = package.Count();
            try {
                for (int currentIndex = 0; currentIndex < packageCount; currentIndex += batchSize) {
                    IEnumerable<T> batch = package.Skip(currentIndex).Take(batchSize);
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

    /// <inheritdoc/>
    public async Task DeleteRangeAsync(IEnumerable<T> entityList) {
        if (entityList == null || !entityList.Any()) return;
        EntitySet.RemoveRange(entityList);
        try { await _context.SaveChangesAsync(); } catch { throw; }
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(long id) {
        T entity;
        if (!EntityExists(id, out entity)) return;
        await DeleteAsync(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(T entity) {
        if (entity == null) return;
        EntitySet.Remove(entity);
        try { await _context.SaveChangesAsync(); } catch { throw; }
    }


    /// <inheritdoc/>
     public bool EntityExists() => EntitySet.Any();


    /// <inheritdoc/>
    public bool EntityExists(long id) => EntitySet.Any(x => x.Id == id);

    #pragma warning disable CS8601

    /// <inheritdoc/>
    public bool EntityExists(long id, out T entity) => (entity = EntitySet.Find(id)) != null;

    #pragma warning restore CS8601

    #endregion

    #region serviceMethods

    /// <summary>
    /// Включить "Жадную загрузку"
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    private IQueryable<T> ApplyEagerLoading(IQueryable<T> query) {
        var entityType = _context.Model.FindEntityType(typeof(T));
        if (entityType == null)
            return query;

        foreach (var navigation in entityType.GetNavigations()) {
            if (navigation.IsOnDependent && navigation.ForeignKey.IsOwnership)
                continue;

            query = query.Include(navigation.Name);
        }

        return query;
    }

    /// <summary>
    /// Получить список имен публичных свойств сущности типа <paramref name="type"/>.
    /// </summary>
    /// <param name="type">Тип сущности, у которой ищем свойства</param>
    /// <returns>Множество наименований свойств сущности <paramref name="type"/></returns>
    private static IEnumerable<string> GetNavigationPropertiesForType(Type? type) {
        if (type == null) return Enumerable.Empty<string>();

        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => typeof(IEntity).IsAssignableFrom(prop.PropertyType))
            .Select(prop => prop.Name);
    }

    #endregion

}