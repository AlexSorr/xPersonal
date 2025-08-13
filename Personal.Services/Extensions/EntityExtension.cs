using System;
using Personal.Models.Model.Base;
using Personal.Service.Managers;

namespace Personal.Services.Extensions;

public static class EntityExtension {

    /// <summary>
    /// Цикл foreach. материализует коллекцию в массив перед выполнением, можно передавать всегда IEnumerable
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> entities, Action<T> action) where T : class, IEntity {
        foreach (var entity in entities.ToArray()) {
            action(entity);
        }
    }

    /// <summary>
    /// Сохранить сущность в БД
    /// </summary>
    public static void Save<T>(this T entity) where T : class, IEntity {
        EntityManager<T>.Instance.Save(entity);
    }

    /// <summary>
    /// Сохранить асинхронно
    /// </summary>
    public static async Task SaveAsync<T>(this T entity) where T : class, IEntity {
        await EntityManager<T>.Instance.SaveAsync(entity);
    }

    /// <summary>
    /// Удалить сущность в БД
    /// </summary>
    public static void Delete<T>(this T entity) where T : class, IEntity {
        EntityManager<T>.Instance.Delete(entity);
    }

    /// <summary>
    /// Удалить асинхронно
    /// </summary>
    public static async Task DeleteAsync<T>(this T entity) where T : class, IEntity {
        await EntityManager<T>.Instance.DeleteAsync(entity);
    }

}
