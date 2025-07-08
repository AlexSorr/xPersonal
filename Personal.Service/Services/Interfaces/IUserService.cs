using Personal.Model.Users;
using Personal.Services.Base;

namespace Personal.Service.Services.Interfaces;

public interface IUserService : IEntityService<User> {

    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Id зарегистрированного пользователя</returns>
    public Task<long> RegistrateUser(User user);

    /// <summary>
    /// Заблокировать пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    public Task BlockUser(User user);

}
