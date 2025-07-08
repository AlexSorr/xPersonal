using System;
using Microsoft.Extensions.DependencyInjection;
using Personal.Model.Base;
using Personal.Services.Base;

namespace Personal.Service.Managers;

/// <summary>
/// Менеджер для работы с сущностями, реализующими <see cref="IEntity"/>
/// </summary>
public static class EntityManager<T> where T : class, IEntity {

    private static IServiceProvider? _serviceProvider;

    public static void Initialize(IServiceProvider provider) => _serviceProvider = provider;

    /// <summary>
    /// Сервис для работы с сущностью <typeparamref name="T"/>
    /// </summary>
    public static IEntityService<T> Instance =>
        _serviceProvider?.GetRequiredService<IEntityService<T>>()
        ?? throw new InvalidOperationException("EntityManager is not initialized.");
}