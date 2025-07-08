using System;

namespace Personal.Model.Task;

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

/// <summary>
/// Типы периодичности
/// </summary>
public enum PeriodType {
    Day,
    Week,
    Month,
    Year
}
