using System.Reflection.Metadata;
using Personal.Model.Base;
using Personal.Model.Wallet;

namespace Personal.Model.Users;

public class User : Entity {

    public User() { }

    public User(string username, UserInfo info, IEnumerable<UserParameter> stats) {
        if (string.IsNullOrWhiteSpace(username) || info == null)
            return;
        Username = username;
        Info = info;
        InitializeStats(stats);
    }

    /// <summary>
    /// Инициализировать параметры пользователя
    /// </summary>
    private void InitializeStats(IEnumerable<UserParameter> stats) => Stats = stats.Select(x => new UserStat(x)).ToList();
        

    public string Username { get; private set; } = string.Empty;

    #region Components

    /// <summary>
    /// Личная информация о пользователе
    /// </summary>
    public UserInfo Info { get; private set; } = null!;

    /// <summary>
    /// Параметры пользователя
    /// </summary>
    public List<UserStat> Stats { get; private set; } = null!;

    /// <summary>
    /// Кошелек пользователя
    /// </summary>
    //public Wallet.Wallet Wallet { get; private set; } = new Wallet.Wallet();

    #endregion

    /// <summary>
    /// Полное имя пользователя
    /// </summary>
    public string FullName => Info?.FullName ?? string.Empty;

    /// <summary>
    /// Уровень пользователя
    /// </summary>
    public int Level { get; private set; } = 1;

    /// <summary>
    /// Отобразить болный баланс денег пользователя
    /// </summary>
    //public string MoneyBalance => this.Wallet.DisplayBalance();

    /// <summary>
    /// Установить имя пользователя
    /// </summary>
    public void SetUsername(string username) => this.Username = !string.IsNullOrWhiteSpace(username) ? username : this.Username;

    public override string ToString() {
        Func<UserStat, string> StatsDisplayString = stat => $" - {stat.Parameter?.Name}: {stat.Parameter?.Level}";
        string stats = string.Join(Environment.NewLine, Stats.Select(StatsDisplayString));

        return $@"
Id: {Id}
Username: {Username}; Age: {Info?.Age}; Level: {Level}
    Firstname: {Info?.FirstName ?? string.Empty}
    Lastname: {Info?.LastName ?? string.Empty}

Stats:
{stats}
        ";
    }

}