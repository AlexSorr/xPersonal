using System.Reflection;
using Personal.Model.Base; 

namespace Personal.Service.Managers;

public static class EntityManagerInitializer {

    public static void InitializeAllEntityManagers(IServiceProvider serviceProvider) {
        var assembly = typeof(IEntity).Assembly;

        var entityTypes = assembly.GetTypes()
            .Where(t =>
                t is { IsClass: true, IsAbstract: false } &&
                typeof(IEntity).IsAssignableFrom(t));

        foreach (var type in entityTypes) {
            var method = typeof(EntityManagerInitializer)
                .GetMethod(nameof(InitializeEntityManagerGeneric), BindingFlags.NonPublic | BindingFlags.Static)?
                .MakeGenericMethod(type);

            method?.Invoke(null, new object[] { serviceProvider });
        }
    }

    private static void InitializeEntityManagerGeneric<T>(IServiceProvider provider) where T : class, IEntity => EntityManager<T>.Initialize(provider);
        
    
}
