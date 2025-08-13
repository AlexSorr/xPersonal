using Personal.Models.Model.Base;



namespace Personal.Models.Model.Users;

public class User : Entity {

    public User() { }

    public User(TelegramUser telegramUser) {
        TelegramUser = telegramUser;
        Info = UserInfo.FromTelegramUser(telegramUser);
        Username = telegramUser.Username ?? string.Empty;
        InitializeParameters();
    }

    #region Properties

    // Username и уровень пользователя
    public string Username { get; private set; } = string.Empty;
    public string FullName => Info?.FullName ?? string.Empty;

    public int Level { get; set; }

    //профили 
    public TelegramUser TelegramUser { get; private set; } = null!;

    public UserInfo Info { get; private set; } = null!;

    public List<UserParameter> Parameters { get; set; } = new List<UserParameter>();
 
    #region ServiceInfo

    /// <summary>
    /// Пользователь заблокирован
    /// </summary>
    public bool IsBlocked { get; set; }
    public DateTime BlockDate { get; set; }


    #endregion

    #endregion

    #region Methods

    public override string ToString() {
        string shortInfo = $"Id: {Info?.Id ?? Guid.Empty} Username: {Username} RegDate: {CreationDate}";
        return shortInfo;
    }

    /// <summary>
    /// Создать параметры пользователя
    /// </summary>
    private void InitializeParameters() {
        var paramTypes = Enum.GetValues(typeof(ParameterType)).Cast<ParameterType>() ?? Enumerable.Empty<ParameterType>();
        
        UserParameter param;
        foreach (var paramType in paramTypes) {
            param = new UserParameter(paramType);
            this.Parameters.Add(param);
        }
    }

    /// TODO проверка валидности переданного имени пользователя
    public void SetUsername(string username) => this.Username = !string.IsNullOrWhiteSpace(username) ? username : this.Username;

    #endregion

}