using System;
using Personal.Models.Model.Base;
using Personal.Models.Model.Events;
using Personal.Models.Model.Users;

namespace Personal.Models.Model.Events;

/// <summary>
/// Базовый интерфейс события пользователя
/// </summary>
public interface IBaseEvent : IUserAttribute, INamedEntity {

    /// <summary>
    /// Описание События
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// Список событий
    /// </summary>
    EventSet? EventSet { get; set; }

}


/// <summary>
/// Иерархическое событие, содержащее подпункты и родителей
/// </summary>
public interface IHierarchicalEvent : IHierarchicalEntity<IBaseEvent> {

    /// <summary>
    /// Подпункты события
    /// TODO посмотреть как реализуется на БД
    /// </summary>
    List<IBaseEvent> SubEvents { get; set; }

}

/// <summary>
/// Повторяющееся событие
/// </summary>
public interface IRepeatableEvent : IBaseEvent {
    
    /// <summary>
    /// TODO пока строкой, продумать как хранить настройку кратности повторения
    /// </summary>
    public string Repetition { get; set; }

}