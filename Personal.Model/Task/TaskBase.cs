using Personal.Model.Base;

namespace Personal.Model.Task;

/// <summary>
/// Базовый класс задачи пользователя
/// </summary>
public abstract class TaskBase : UserAttribute, ITaskBase {

    public TaskBase() { }

    public TaskBase(string name) { Name = name; }

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

/// <summary>
/// Интерфейс базового базовой задачи
/// </summary>
public interface ITaskBase : IUserAttribute {

    /// <summary>
    /// Завершить задачу
    /// </summary>
    void Complete();

}

/// <summary>
/// Класс периодической задачи, которая выполняется раз в период
/// </summary>
public class PeriodicTask : TaskBase {

    public PeriodicTask() { }

    /// <summary>
    /// Период задачи
    /// </summary>
    public TaskPeriod Period { get; set; } = new TaskPeriod();

}

/// <summary>
/// Типы периодичности
/// </summary>
public enum PeriodType {
    Day,
    Week,
    Month,
    Year
}

/// <summary>
/// Класс, описывающий периодичность <see cref="PeriodicTask"/>
/// </summary>
public class TaskPeriod {

    public TaskPeriod() { }

    /// <summary>
    /// Количество - сколько раз нужно повторить задачу
    /// </summary>
    public int TaskCount { get; private set; } = 0;

    /// <summary>
    /// Во сколько дней/нед/мес/лет
    /// </summary>
    public int PeriodCount { get; private set; } = 0;

    /// <summary>
    /// Тип периодичности - дни/недели/месяцы
    /// </summary>
    public PeriodType PeriodType { get; private set; }

    public TaskPeriod(int TaskCount, int PeriodCount, PeriodType PeriodType) { 
        if (TaskCount <= 0 || PeriodCount <= 0) return;
        this.TaskCount = TaskCount;
        this.PeriodCount = PeriodCount;
        this.PeriodType = PeriodType;
    }

}