namespace Personal.Model.Wallet;

/// <summary>
/// Класс, описывающий перевод между счетами
/// </summary>
public class TransactionAction : AccountAction {

    /// <summary>
    /// Счет, на который осуществляется перевод
    /// </summary>
    public Account DestinationAccount { get; private set; }

    public TransactionAction(Account accountFrom, Account destinationAccount, decimal amount) : base(accountFrom, amount) {
        DestinationAccount = destinationAccount;
    }

    /// <inheritdoc/>
    public override void Execute() { }

}
