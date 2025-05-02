using Microsoft.EntityFrameworkCore;
using Personal.Data;
using Personal.Testing;

string Host = "localhost";
string Database = "personal_db";
string Username = "postgres";
string Password = "";

while (string.IsNullOrWhiteSpace(Password))
    Password = Helper.GetRequested(nameof(Password));

string connectionString = $"Host={Host};Database={Database};Username={Username};Password={Password}";

var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseNpgsql(connectionString)
    .Options;

using var db = new ApplicationDbContext(options);
var creator = new UserLoader(db);
await creator.StartRegister();