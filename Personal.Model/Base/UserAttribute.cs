using Personal.Model.Users;

namespace Personal.Model.Base;

/// <summary>
/// Класс, описывающий атрибут пользователя
/// </summary>
public abstract class UserAttribute : Entity, IUserAttribute {
    
    /// <inheritdoc/>
    public User User { get; } = new User();

}
