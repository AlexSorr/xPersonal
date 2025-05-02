using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

using System.Reflection;

using Personal.Model.Base;
using Personal.Model;
using Personal.Model.Users;
using System.Reflection.Metadata;


namespace Personal.Data;

/// <summary>
/// Контекст базы данных для работы с сущностями.
/// Содержит настройки для управления сущностями и конфигурацию базы данных.
/// </summary>
public class ApplicationDbContext : DbContext {
    
    private readonly ILogger<ApplicationDbContext>? _logger;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ApplicationDbContext"/> с заданными параметрами.
    /// </summary>
    /// <param name="options">Параметры конфигурации контекста.</param>
    /// <param name="logger">Логгер для записи информации о работе контекста.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) : base(options) { 
        _logger = logger; 
    }

    //Конфиг модели
    #region ModelConfig

    /// <summary>
    /// Настраивает параметры модели и устанавливает конфигурацию БД.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели для настройки сущностей.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        ApplyEntityKeyConfiguration(modelBuilder);
        ApplyForeignKeyConfiguration(modelBuilder);
        SetDbObjectNamesLowerInvariantCase(modelBuilder);
    }

    /// <summary>
    /// Приводит имена таблиц и столбцов к нижнему регистру для поддержания единообразия.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели для настройки сущностей.</param>
    private void SetDbObjectNamesLowerInvariantCase(ModelBuilder modelBuilder) {
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes()) {
            string tableName = entity.GetTableName() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(tableName)) {
                _logger?.LogError($"Table name for entity {entity.GetType()} not found");
                continue;
            }
            entity.SetTableName(tableName.ToLowerInvariant());

            foreach (IMutableProperty property in entity.GetProperties())
                property.SetColumnName(property.Name.ToLowerInvariant());
        }
    }

    #endregion

    //Сущности, хранящиеся в БД
    #region DbSets

    /// <summary>
    /// Сет с пользователями
    /// </summary>
    public DbSet<User> User { get; set; }

    public DbSet<UserInfo> UserInfo { get; set; }

    /// <summary>
    /// Справочник параметров пользователя
    /// </summary>
    public DbSet<UserParameter> UserParameters { get; set; }

    /// <summary>
    /// Таблица статов конкретных пользователей
    /// </summary>
    public DbSet<UserStat> UserStat { get; set; }

    #endregion

    //настройка связей между объектами
    #region PrimaryForeignKeysConfig

    /// <summary>
    /// Настраивает первичные ключи для всех сущностей, реализующих интерфейс <see cref="IEntity"/>.
    /// </summary>
    /// <param name="modelBuilder">Построитель модели для настройки сущностей.</param>
    private void ApplyEntityKeyConfiguration(ModelBuilder modelBuilder) {
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(IEntity).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        foreach (Type type in types) 
            modelBuilder.Entity(type).HasKey("Id");
    }

    /// <summary>
    /// Настраивает параметры внешних ключей
    /// </summary>
    /// <param name="modelBuilder">Построитель модели для настройки сущностей.</param>
    private void ApplyForeignKeyConfiguration(ModelBuilder modelBuilder) {
        ApplyPrivateFkColumns(modelBuilder);

        // User → UserInfo (1:1)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Info)
            .WithOne(i => i.User)
            .HasForeignKey<UserInfo>("_userId"); 

        // User → UserStat[] (1:N)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Stats)
            .WithOne(stat => stat.User)
            .HasForeignKey("_userId");

        // UserStat → UserParameter (1:1)
        modelBuilder.Entity<UserStat>()
            .HasOne(s => s.Parameter)
            .WithMany()
            .HasForeignKey("_parameter"); 
        

    }

    /// <summary>
    /// Применить настройки для приватных внешних ключей - задать имя колонок
    /// TODO Какого то хуя не работает, колонки с дебильными именами создаются, надо решить
    /// </summary>
    /// <param name="modelBuilder">Построитель модели для настройки сущностей</param>
    private void ApplyPrivateFkColumns(ModelBuilder modelBuilder) {
        modelBuilder.Entity<UserInfo>().Property<long>("_userId").HasColumnName("UserId");
        modelBuilder.Entity<UserStat>().Property<long>("_userId").HasColumnName("UserId");

        modelBuilder.Entity<UserStat>().Property<long>("_parameter").HasColumnName("Parameter");
    }

    #endregion

}
