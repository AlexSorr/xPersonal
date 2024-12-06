using Personal.Model.Base;
using Personal.Model.Wallet;

namespace Personal.Model.Users;

public class User : Entity {

    public User() { }

    public string Username { get; private set; } = string.Empty;

    #region Components

    /// <summary>
    /// Личная информация о пользователе
    /// </summary>
    public UserInfo Info { get; private set; } = new UserInfo();

    /// <summary>
    /// Параметры пользователя
    /// </summary>
    public UserStats Stats { get; private set; } = new UserStats();

    /// <summary>
    /// Кошелек пользователя
    /// </summary>
    public Wallet.Wallet Wallet { get; private set; } = new Wallet.Wallet();

    #endregion

    /// <summary>
    /// Полное имя пользователя
    /// </summary>
    public string FullName => Info?.FullName ?? string.Empty;

    /// <summary>
    /// Уровень пользователя
    /// </summary>
    public int Level => Stats?.Level ?? 0; 

    /// <summary>
    /// Отобразить болный баланс денег пользователя
    /// </summary>
    public string MoneyBalance => this.Wallet.DisplayBalance();

}

