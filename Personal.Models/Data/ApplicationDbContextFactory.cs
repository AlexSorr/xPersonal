using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Personal.Models.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> {

    public ApplicationDbContext CreateDbContext(string[] args)  {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // FIXME вынести конфиг БД куда-то
        optionsBuilder.UseNpgsql("Host=localhost;Database=personal_db;Username=postgres;Password=");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

