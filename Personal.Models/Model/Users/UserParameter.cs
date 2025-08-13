using Personal.Models.Model.Base;

namespace Personal.Models.Model.Users;

/// <summary>
/// Типы параметров
/// </summary>
public enum ParameterType {
    Health = 1,
    Guts = 2,
    Knowledge = 3,
    Comfort = 4,
    Profifiency = 5
}

/// <summary>
/// Параметр пользователя
/// </summary>
public class UserParameter : UserAttribute {

    public UserParameter() { }

    public UserParameter(ParameterType type) {
        Type = type;
        Name = type.ToString();
    }

    public string Name { get; set; } = string.Empty;

    public ParameterType Type { get; set; }

    public int Level { get; private set; } = 0;

    public long Exp { get; set; } = 0;

    public void UpdateLevel() {}

}
