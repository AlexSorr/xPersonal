using Personal.Model.Base;
using Personal.Model.Users;

namespace Personal.Model.Task;

/// <summary>
/// Базовый класс задачи пользователя
/// </summary>
public class TaskBase : Entity, ITaskBase, IUserAttribute {

    public TaskBase() { }

    public TaskBase(string name) { Name = name; }

    /// <inheritdoc/>
    public User User { get; set; } = null!;

    /// <summary>
    /// Наименование задачи
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Задача завершена
    /// </summary>
    public bool IsCompleted { get; private set; }

    /// <summary>
    /// Дата завершения задачи
    /// </summary>
    public DateTime? FinishDate { get; private set; }

    //Что бафает, какой параметр?

    /// <inheritdoc/>
    public void Complete() { }

}