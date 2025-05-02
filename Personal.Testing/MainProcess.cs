using Personal.Model.Base;
using Personal.Model.Users;
using Personal.Services.Base;

namespace Personal.Testing.API;

public class MainProcess {

    private readonly IEntityServiceFactory _factory;

    public MainProcess(IEntityServiceFactory factory) => _factory = factory;

    private IEntityService<T> GetEntityService<T>() where T : class, IEntity => _factory.Create<T>();

    public async Task GetUserByIdAsync() {
        long user_id;
        string id = Helper.GetRequested("user id").Trim();
        while (!long.TryParse(id, out user_id))
            id = Helper.GetRequested("user id").Trim();
        
        var res = await GetEntityService<User>().LoadByIdAsync(user_id);
        if (res == null) {
            System.Console.WriteLine("User not found");
            return;
        }
        System.Console.WriteLine(res.ToString());
        System.Console.ReadKey();    
    }

    public async Task AddNewUser() {
        var loader = new UserLoader(_factory);
        await loader.CreateUser();
    }
}