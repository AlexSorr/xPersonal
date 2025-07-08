using System.Text;
using Microsoft.Extensions.Logging;
using Personal.Data;
using Personal.Model.Base;
using Personal.Model.Users;
using Personal.Service.Services.Interfaces;
using Personal.Services.Base;

namespace Personal.Service.Services.Base;

public class UserService : EntityService<User>, IUserService {

    public UserService(ApplicationDbContext context, ILogger<EntityService<User>> logger, IEntityServiceFactory serviceFactory) : base(context, logger, serviceFactory) { }

    /// <inheritdoc/>
    public async Task<long> RegistrateUser(User user) {
        long userId = 0;

        if (user == null) return 0;
        
        if (long.TryParse(user.Id + string.Empty, out userId) && EntityExists(userId)) 
            throw new Exception($"Пользователь c Id {userId} уже существует в системе;{Environment.NewLine}\t{user.ShortInfo}");
        
        if (!UserDataIsValid(user, out string error))
            throw new Exception($"Некорректные данные пользователя{Environment.NewLine} {error}");

        await SaveAsync(user);
        return user.Id; //EF присвоит самостоятельно
    }

    /// <summary>
    /// Установить блокировку на пользователя
    /// </summary>
    private Action<User> SetBlocking = user => { user.IsBlocked = true; user.BlockDate = DateTime.UtcNow; };

    /// <inheritdoc/>
    public async Task BlockUser(User user) {
        if (user == null || user.IsBlocked) return;
        SetBlocking(user);
        await SaveAsync(user);
    }

    /// <inheritdoc/>
    public async Task BlockUser(IEnumerable<User> users) {
        User[] blockList = users.Where(x => !(x?.IsBlocked ?? true)).ToArray();
        foreach (var user in blockList) 
            SetBlocking(user);
        await SaveBatchAsync(blockList);
    }

    /// <summary>
    /// Проверить, что у пользователя заполнены все необходимые для регистрации данные:
    /// - Username
    /// - Name
    /// - BirthDate
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="error">Ошибки валидации</param>
    /// <returns><c>true</c> если все данные корректны</returns>
    protected bool UserDataIsValid(User user, out string error) {
        error = string.Empty;
        StringBuilder errors = new StringBuilder();

        if (user == null) {
            error = "Пользователь null";
            return false;
        }
        
        //TODO переформулировать, а то два раза подряд "имя пользователя"
        if (string.IsNullOrWhiteSpace(user.Username))
            errors.AppendLine("Не заполнено имя пользователя");
        
        // если Firstname не пустой, то и имя не пустое, поэтому можно чекать так
        if (string.IsNullOrWhiteSpace(user.FullName)) 
            errors.AppendLine("Не заполнено имя пользователя");

        if (!DateTime.TryParse(user.Info?.BirthDate + string.Empty, out _))
            errors.AppendLine("Не указана дата рождения пользователя");

        error = errors.ToString();

        return string.IsNullOrWhiteSpace(error);
    }

}
