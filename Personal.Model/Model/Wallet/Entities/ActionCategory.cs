using System;
using Personal.Model.Base;

namespace Personal.Model.Wallet;

public class ActionCategory : Entity, IHierarchicalEntity<ActionCategory> {

    public string Name { get; set; } = string.Empty;

    public ActionCategory? Parent { get; set; }

}
