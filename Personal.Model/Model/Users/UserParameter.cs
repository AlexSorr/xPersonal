using Personal.Model.Base;

namespace Personal.Model.Users;

public class UserParameter : Entity {

    public string Name { get; set; } = string.Empty;

    public int Level { get; private set; } = 0;

    public long Exp { get; set; } = 0;

    public void UpdateLevel() {}

}
