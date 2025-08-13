using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Reflection;

using Personal.Models.Model.Base;
using Personal.Models.Model.Users;


namespace Personal.Models.Data;

/// <summary>
/// Контекст базы данных для работы с сущностями.
/// Содержит настройки для управления сущностями и конфигурацию базы данных.
/// </summary>
public class ApplicationDbContext : DbContext {
    
    private readonly ILogger<ApplicationDbContext>? _logger;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) : base(options) { 
        _logger = logger; 
    }

    //Конфиг модели
    #region ModelConfig

    /// <summary>
    /// Настраивает параметры модели и устанавливает конфигурацию БД.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        ApplyEntityKeyConfiguration(modelBuilder);
        ApplyForeignKeyConfiguration(modelBuilder);
    }


    #endregion

    //Сущности, хранящиеся в БД
    #region DbSets

    /// <summary>
    /// Сет с пользователями
    /// </summary>
    public DbSet<User> User { get; set; }

    /// <summary>
    /// Инфа о пользователе
    /// </summary>
    public DbSet<UserInfo> UserInfo { get; set; }

    /// <summary>
    /// Telegram пользователи
    /// </summary>
    public DbSet<TelegramUser> TelegramUser { get; set; }

    /// <summary>
    /// Параметры пользователя
    /// </summary>
    public DbSet<UserParameter> UserParameters { get; set; }

    #endregion

    //настройка связей между объектами
    #region PrimaryForeignKeysConfig

    /// <summary>
    /// Настраивает первичные ключи для всех сущностей, реализующих интерфейс <see cref="IEntity"/>.
    /// </summary>
    private void ApplyEntityKeyConfiguration(ModelBuilder modelBuilder) {
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IEntity).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        foreach (Type type in types) 
            modelBuilder.Entity(type).HasKey("Id");
    }

    /// <summary>
    /// Настраивает параметры внешних ключей
    /// </summary>
    private void ApplyForeignKeyConfiguration(ModelBuilder modelBuilder) {

        // Свойства пользователя
        modelBuilder.Entity<User>(entity => {
            entity.HasOne(user => user.TelegramUser)
                .WithOne(tgUser => tgUser.User)
                .HasForeignKey<TelegramUser>(user => user.Id)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(user => user.Info)
                .WithOne(info => info.User)
                .HasForeignKey<UserInfo>(user => user.Id)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(user => user.Parameters)
                .WithOne(param => param.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //автовыдача Guid-а
            //entity.Property(u => u.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
        });


    }

    #endregion

}
