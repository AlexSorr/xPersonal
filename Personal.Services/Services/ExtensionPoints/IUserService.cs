using Personal.Models.Model.Users;
using Personal.Services.Base;
using Personal.Services.Models.Responses;

namespace Personal.Services.ExtensionPonints;

public interface IUserService : IEntityService<User> {

    /// <summary>
    /// Существует пользователь с таким TelegramId
    /// </summary>  
    Task<bool> TelegramUserExistsAsync(long telegramUserId);


    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    Task<RegistrationResult> RegistrateUserAsync(TelegramUser telegramUser);


    /// <summary>
    /// Проверить данные для регистрации пользователя, обновить RegistrationResult
    /// </summary>
    /// <returns>
    /// Строка с ошибками валидации. Пустая строка если все ок
    /// - Пользователь с таким TelegramId не существует
    /// - Username в системе не занят
    /// </returns>
    Task ValidateRegistrationDataAsync(RegistrationResult regResult);


    /// <summary>
    /// Заблокировать пользователя
    /// </summary>
    Task BlockUser(User user);

}
