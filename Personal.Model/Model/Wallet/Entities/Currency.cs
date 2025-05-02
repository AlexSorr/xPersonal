using Personal.Model.Base;

namespace Personal.Model.Wallet;

public class Currency : Entity {

    public Currency() {}

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

}
