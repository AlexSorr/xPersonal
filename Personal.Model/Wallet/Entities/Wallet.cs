using Personal.Model.Base;

namespace Personal.Model.Wallet;

/// <summary>
/// Кошелек пользователя
/// </summary>
public class Wallet : UserAttribute, IBalanceHolder {

    public Wallet() {}

    /// <summary>
    /// Баланс в основной валюте приложения
    /// </summary>
    public decimal Balance { get; }

    /// <summary>
    /// Основная валюта
    /// </summary>
    public Currency Currency { get; } = new Currency();

    /// <inheritdoc/>
    public string DisplayBalance() => $"{Balance} {Currency?.Code}";

    /// <summary>
    /// Список счетов пользователя
    /// </summary>
    public List<Account> AccountList { get; } = new List<Account>();

}