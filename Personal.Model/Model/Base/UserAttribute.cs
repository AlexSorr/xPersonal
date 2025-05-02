using Personal.Model.Users;

namespace Personal.Model.Base;

/// <summary>
/// Класс, описывающий атрибут пользователя
/// </summary>
public abstract class UserAttribute : Entity, IUserAttribute {
    
    #pragma warning disable CS0169
    
    private long _userId;

    #pragma warning restore CS0169

    /// <inheritdoc/>
    /// TODO сделать реализацию, завязанную на _userId
    public User User { get; set; } = null!;

}
