using Personal.Model.Base;

namespace Personal.Model.Wallet;

/// <summary>
/// Класс, описывающий счет пользователя
/// </summary>
public class Account : UserAttribute, IBalanceHolder {

    public Account() { }

    /// <summary>
    /// Наименование счета
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc/>
    public decimal Balance { get; set; } = 0;

    /// <inheritdoc/>
    public Currency Currency { get; set; } = new Currency();

    /// <inheritdoc/>
    public string DisplayBalance() => $"{Balance} {Currency?.Code}";

}