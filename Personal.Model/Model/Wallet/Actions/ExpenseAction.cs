
namespace Personal.Model.Wallet;

/// <summary>
/// Класс, описывающий расход по счету
/// </summary>
public class ExpenseAction : AccountAction {

    public ExpenseAction(Account account, decimal sum) : base(account, sum) { }

    /// <inheritdoc/>
    public override void Execute() { }

}
