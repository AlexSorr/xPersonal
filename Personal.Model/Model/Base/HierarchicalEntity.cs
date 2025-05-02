using System;

namespace Personal.Model.Base;

/// <summary>
/// Иерархическая сущность
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public abstract class HierarchicalEntity<T> : IHierarchicalEntity<T> where T : class, IEntity {

    #pragma warning disable CS0169

    private long _parentId;

    #pragma warning restore CS0169
    
    public T? Parent { get; set; } 

}
