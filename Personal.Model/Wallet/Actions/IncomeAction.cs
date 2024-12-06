namespace Personal.Model.Wallet;

public class IncomeAction : AccountAction {

    public IncomeAction(Account account, decimal sum) : base(account, sum) { }

    /// <inheritdoc/>
    public override void Execute() {
        if (Account == null || Sum <= 0) return;
        Account.Balance += Sum;
    }

}
