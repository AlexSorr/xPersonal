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
string Password = "194352Me";

while (string.IsNullOrWhiteSpace(Password))
    Password = Helper.GetRequested(nameof(Password));

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

    // Запуск вашей логики
    var processor = host.Services.GetRequiredService<MainProcess>();
    await processor.AddNewUser();


//Загрузка пользователя тестова

//using var db = new ApplicationDbContext(options);
//var creator = new UserLoader(db);
//await creator.StartRegister();