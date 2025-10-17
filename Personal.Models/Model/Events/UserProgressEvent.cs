using System;
using Personal.Models.Model.Users;

namespace Personal.Models.Model.Events;

/// <inheritdoc/>
public class UserProgressEvent : BaseEvent, IUserProgressEvent {
    
    /// <inheritdoc/>
    public Dictionary<UserParameter, int> Changes { get; set; } = new Dictionary<UserParameter, int>();

}