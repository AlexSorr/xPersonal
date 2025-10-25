using System;

namespace Personal.Models.Model.Base;

/// <summary>
/// Сущность, имеющая имя (доступное для понимания пользователю)
/// </summary>
public class NamedEntity : Entity {

    public string Name { get; set; } = "<Без названия>";

}
