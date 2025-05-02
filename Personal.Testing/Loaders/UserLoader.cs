using Personal.Model.Users;
using Personal.Data;
using System.Security.Cryptography;
using Personal.Model.Base;
using Personal.Services.Base;

namespace Personal.Testing;

/// <summary>
/// Тестовый загрузчик пользователя
/// </summary>
public class UserLoader {

    private readonly IEntityServiceFactory _factory;

    public UserLoader(IEntityServiceFactory factory) => _factory = factory;

    private IEntityService<T> GetEntityService<T>() where T : class, IEntity => _factory.Create<T>();

    /// <summary>
    /// Вручную завести пользователя
    /// </summary>
    /// <returns></returns>
    public async Task CreateUser() {
        await CheckAndCreateUserParameters();

        string username = Helper.GetRequested(nameof(username));
        while (string.IsNullOrWhiteSpace(username)) 
            username = Helper.GetRequested(nameof(username));
        
        UserInfo info = RequestPersonalInfo();
        var stats = GetEntityService<UserParameter>().All();

        var user = new User(username, info, stats);
        await RequsetUserSaving(user);
    }

    private async Task CheckAndCreateUserParameters() {
        var service = GetEntityService<UserParameter>();
        if (!service.EntityExists()) {
            var parameters = new [] {
                new UserParameter() { Name = "Health" },
                new UserParameter() { Name = "Comfort" },
                new UserParameter() { Name = "Proficiency" }
            };
            await service.SaveBatchAsync(parameters);
        }
    }

    private UserInfo RequestPersonalInfo() {
        string FirstName = Helper.GetRequested(nameof(FirstName));
        while (string.IsNullOrWhiteSpace(FirstName))
            FirstName = Helper.GetRequested(nameof(FirstName));
        
        string LastName = Helper.GetRequested(nameof(LastName));
        while (string.IsNullOrWhiteSpace(LastName))
            LastName = Helper.GetRequested(nameof(LastName));

        DateTime date = default;  
        string BirthDate = Helper.GetRequested(nameof(BirthDate));
        while (!DateTime.TryParse(BirthDate, out date))
            BirthDate = Helper.GetRequested(nameof(BirthDate));

        return new UserInfo(FirstName, LastName, date);
    }

    private async Task RequsetUserSaving(User user) {
        Console.Clear();
        if (user == null) return;

        System.Console.WriteLine(user.ToString());
        System.Console.Write("Save? [y/n]");

        string res = (System.Console.ReadLine() ?? string.Empty).Trim().ToLower();
        while (!new [] { "y", "n" }.Contains(res)) 
            res = (System.Console.ReadLine() ?? string.Empty).Trim().ToLower();

        string message = "Canceled";
        if (res == "y") {
            var service = GetEntityService<User>();
            await service.SaveAsync(user);
            message = $"User {user?.FullName} saved sucessfuly";
        }
        System.Console.WriteLine(message);
    }
}
