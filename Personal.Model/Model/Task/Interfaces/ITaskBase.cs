using Personal.Model.Base;

namespace Personal.Model.Task;

/// <summary>
/// Интерфейс базовой задачи
/// </summary>
public interface ITaskBase : IEntity {
    
    /// <summary>
    /// Завершить задачу
    /// </summary>
    void Complete();

}
