using Personal.Model.Users;

namespace Personal.Model.Base;

/// <summary>
/// Интерфейс, описывающий атрибут пользователя
/// </summary>
public interface IUserAttribute : IEntity {
    
    /// <summary>
    /// Пользователь(владелец атрибута)
    /// </summary>
    public User User { get; }

}
