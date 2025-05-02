using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Personal.Data;
using Personal.Testing;
using Personal.API.Services.Base;
using Microsoft.Extensions.Hosting;
using Personal.Testing.API;

string Hostname = "localhost";
string Database = "personal_db";
string Username = "postgres";
string Password = "";

while (string.IsNullOrWhiteSpace(Password))
    Password = Helper.GetRequested($"database {nameof(Password)}");

string connectionString = $"Host={Hostname};Database={Database};Username={Username};Password={Password}";


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
        services.AddSingleton<IEntityServiceFactory, EntityServiceFactory>();

        services.AddTransient<MainProcess>();
    })
    .Build();

System.Console.Clear();

var processor = host.Services.GetRequiredService<MainProcess>();

int commandId;
string command = Helper.GetRequested(nameof(command));
while (!int.TryParse(command, out commandId))
    command = Helper.GetRequested(nameof(command));

switch (commandId) {
    case 1:
        await processor.AddNewUser();
        break;
    case 2:
        await processor.GetUserByIdAsync();
        break;
    default:
        break;
}