using Personal.Model.Base;

namespace Personal.Model.Users;

/// <summary>
/// Класс, описывающий параметры пользователя
/// </summary>
public class UserStats : UserAttribute {

    public UserStats() {}

    /// <summary>
    /// Уровень пользователя
    /// </summary>
    public int Level { get; private set; } 

    /// <summary>
    /// Список показателей пользователя
    /// </summary>
    public List<UserParameter> Parameters { get; set; } = new List<UserParameter>();

}
