namespace Personal.Model.Wallet;

/// <summary>
/// Интерфейс, представляющий действие со счетом
/// </summary>
public interface IAccountAction {
    
    /// <summary>
    /// Счет по которому выполняется транзакция
    /// </summary>
    Account Account { get; }

    /// <summary>
    /// Сумма, на которую выполняется транзакция
    /// </summary>
    decimal Sum { get; }

    /// <summary>
    /// Выполнить транзакцию
    /// </summary>
    void Execute();

}
