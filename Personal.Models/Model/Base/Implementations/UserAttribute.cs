using Personal.Models.Model.Users;

namespace Personal.Models.Model.Base;

/// <summary>
/// Класс, описывающий атрибут пользователя
/// </summary>
public abstract class UserAttribute : Entity, IUserAttribute {
    
    /// <inheritdoc/>
    public Guid UserId { get; set; } 

    /// <inheritdoc/>
    public User User { get; set; } = null!;

}
