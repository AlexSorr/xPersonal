using System;
using Personal.Models.Model.Users;

namespace Personal.Models.Model.Events;

/// <summary>
/// Событие, завершение которого влияет на параметры пользователя
/// </summary>
public interface IUserProgressEvent : IBaseEvent {
    
    /// <summary>
    /// Параметр: на сколько повышается
    /// </summary>
    Dictionary<UserParameter, int> Changes { get; set; }
    
}
