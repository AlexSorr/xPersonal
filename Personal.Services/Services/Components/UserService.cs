using System.Text;
using Microsoft.Extensions.Logging;


using Personal.Models.Model.Users;
using Personal.Models.Data;

using Personal.Services.ExtensionPonints;
using Personal.Services.Base;
using Personal.Services.Extensions;
using Personal.Services.Models.Responses;

namespace Personal.Services.Components;

public class UserService : EntityService<User>, IUserService {

    public UserService(ApplicationDbContext context, ILogger<EntityService<User>> logger, IEntityServiceFactory serviceFactory) : base(context, logger, serviceFactory) { }
    
    /// <inheritdoc/>
    public async Task<bool> TelegramUserExistsAsync(long telegramUserId) {
        return await EntityExistsAsync(x => x.TelegramUser != null && x.TelegramUser.TelegramId == telegramUserId);
    }


    /// <inheritdoc/>
    public async Task<RegistrationResult> RegistrateUserAsync(TelegramUser telegramUser) {
        var user = new User(telegramUser);
        var result = new RegistrationResult(user);
        await ValidateRegistrationDataAsync(result);

        if (result.Success)
            await user.SaveAsync();
        
        return result;
    }



    /// <inheritdoc/>
    public async Task ValidateRegistrationDataAsync(RegistrationResult registrationResult) {
        if (registrationResult == null)
            return;

        var user = registrationResult.User;

        if (user?.TelegramUser == null) {
            registrationResult.Description = "Передан пустой пользоватль или не заполнены данные telegram";
            registrationResult.Success = false;
            return;
        }

        var errorBuilder = new StringBuilder();

        bool tgUserExists = await TelegramUserExistsAsync(user.TelegramUser.TelegramId);

        if (tgUserExists) {
            registrationResult.Description = "Такой telegram-пользователь уже зарегистрирован";
            registrationResult.Success &= false;
            return;
        }
        
        string username = (!user.Username.IsNullOrWhiteSpaces() ? user.Username : user.TelegramUser.Username).Trim();
        if (username.IsNullOrWhiteSpaces()) {
            registrationResult.Description = "Не заполнено имя пользователя";
            registrationResult.Success &= false;
            return;
        }

        bool usernameTaken = await EntityExistsAsync(x => x.Username.Trim() == username);
        if (usernameTaken) {
            registrationResult.Description = $"Имя пользователя {username} уже занято";
            registrationResult.Success &= false;
            return;
        }
    
    } 
    


    /// Установить блокировку на пользователя
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
        blockList.ForEach(SetBlocking);
        await SaveBatchAsync(blockList);
    }

}
