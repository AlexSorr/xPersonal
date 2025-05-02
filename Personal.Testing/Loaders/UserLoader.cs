using Personal.Model.Users;
using Personal.Data;
using System.Security.Cryptography;

namespace Personal.Testing;

/// <summary>
/// Тестовый загрузчик пользователя
/// </summary>
public class UserLoader {

    private readonly ApplicationDbContext _db;
    private User? result;

    public UserLoader(ApplicationDbContext db) { _db = db; }

    public async Task StartRegister() {
        if (!_db.Set<UserParameter>().Any())
            await CreateParameters();

        string username = Helper.GetRequested(nameof(username));
        while (string.IsNullOrWhiteSpace(username)) 
            username = Helper.GetRequested(nameof(username));

        UserInfo info = RequestPersonalInfo();
        var stats = _db.Set<UserParameter>();

        result = new User(username, info, stats);

        await RequsetUserSaving(result);

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

    private async Task CreateParameters() {
        var parameters = new [] {
            new UserParameter() { Name = "Health" },
            new UserParameter() { Name = "Comfort" },
            new UserParameter() { Name = "Proficiency" }
        };
        _db.Set<UserParameter>().AddRange(parameters);
        await _db.SaveChangesAsync();
        Console.WriteLine("Parameters created");
    }

    private async Task RequsetUserSaving(User user) {
        Console.Clear();
        if (user == null) return;

        System.Console.WriteLine(user.ToString());
        System.Console.Write("Save? [y/n]");
        string res = System.Console.ReadLine() ?? string.Empty;
        while (string.IsNullOrWhiteSpace(res) || res.Trim().ToLower() != "y") 
            res = System.Console.ReadLine() ?? string.Empty;

        string message = "Canceled";
        if (res == "y") {
            await _db.Set<User>().AddAsync(user);
            await _db.SaveChangesAsync();
            message = $"User {user?.FullName} saved sucessfuly";
        }
        System.Console.WriteLine(message);
    }

}
