using Personal.Model.Base;

namespace Personal.Model.Users;

/// <summary>
/// Класс, описывающий параметры пользователя
/// </summary>
public class UserStat : UserAttribute {

    public UserStat() { }

    public UserStat(UserParameter parameter, int level = 0) {
        Parameter = parameter;
        Level = level;
    }

    /// <summary>
    /// Уровень пользователя
    /// </summary>
    public int Level { get; private set; } = 0;

    #pragma warning disable CS0169
    private long _parameter;

    /// <summary>
    /// Показатель пользователя
    /// </summary>
    public UserParameter Parameter { get; set; } = null!;

    #pragma warning restore CS0169

}
