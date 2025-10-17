using System;
using Personal.Models.Model.Base;
using Personal.Models.Model.Users;

namespace Personal.Models.Model.Events;

public class BaseEvent : UserAttribute, IBaseEvent {

    public BaseEvent() { }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public EventSet? EventSet { get; set; } = null;
}

/// <summary>
/// Список событий пользователя
/// </summary>
public class EventSet : UserAttribute {

    public List<IBaseEvent> Events { get; set; } = new List<IBaseEvent>();

}



// builder.OwnsOne(t => t.UserProgressEvent, e =>
// {
//     e.Property(p => p.Changes)
//         .HasColumnType("jsonb")
//         .HasConversion(
//             v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
//             v => JsonSerializer.Deserialize<Dictionary<UserParameter, int>>(v, (JsonSerializerOptions?)null)!);
// });
