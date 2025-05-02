namespace Personal.Model.Wallet;

/// <summary>
/// Интерфейс, представляющий объект, который имеет баланс
/// </summary>
public interface IBalanceHolder {

    /// <summary>
    /// Баланс
    /// </summary>
    decimal Balance { get; }

    /// <summary>
    /// Валюта
    /// </summary>
    Currency Currency { get; }

    /// <summary>
    /// Отобразить баланс
    /// </summary>
    /// <returns>Строковое представление баланса</returns>
    abstract string DisplayBalance();

}