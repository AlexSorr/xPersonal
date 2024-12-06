using Personal.Model.Base;

namespace Personal.Model.Users;

public class UserParameter : UserAttribute {

    public string Name { get; set; } = string.Empty;

    public int Level { get; private set; } = 0;

    public int Exp { get; set; } = 0;

    public void UpdateLevel() {}

}
