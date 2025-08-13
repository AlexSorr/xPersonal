using Personal.Models.Model.Users;

namespace Personal.Models.Model.Base;

/// <summary>
/// Интерфейс, описывающий атрибут пользователя
/// </summary>
public interface IUserAttribute : IEntity {

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь(владелец атрибута)
    /// </summary>
    public User User { get; set; }

}
