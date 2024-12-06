
namespace Personal.Model.Wallet;

/// <summary>
/// Абстрактный класс, описывающий действие со счетом 
/// </summary>
public abstract class AccountAction : IAccountAction {

    /// <summary>
    /// Базовый конструктор для действия со счетом
    /// </summary>
    /// <param name="account">Счет, к которому будет применено действие</param>
    /// <param name="sum">Сумма, на которую совершается действие</param>
    public AccountAction(Account account, decimal sum) { 
        if (account == null || sum <= 0) return;
        Account = account;
        Sum = sum;
    }

    /// <inheritdoc/>    
    public Account Account { get; private set; } = new Account();

    /// <inheritdoc/>
    public decimal Sum { get; private set; } = 0;

    /// <inheritdoc/>
    public abstract void Execute();

}
