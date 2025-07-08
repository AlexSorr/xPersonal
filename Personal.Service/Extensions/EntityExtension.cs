using System;
using Personal.Model.Base;
using Personal.Model.Users;
using Personal.Service.Managers;
using Personal.Services;

namespace Personal.Service.Extensions;

public static class EntityExtension {

    /// <summary>
    /// Сохранить асинхронно
    /// </summary>
    public static Task SaveAsync<T>(this T entity) where T : Entity {
        return EntityManager<T>.Instance.SaveAsync(entity);
    }

    /// <summary>
    /// Удалить асинхронно
    /// </summary>
    public static Task DeleteAsync<T>(this T entity) where T : Entity {
        return EntityManager<T>.Instance.DeleteAsync(entity);
    }

    /// <summary>
    /// Пройтись циклом по <c>not-null</c> значениям
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> entities, Action<T> action) where T : Entity {
        var array = entities.Where(x => x != null).ToArray();
        foreach (var entity in array) action(entity);
    }

}
