
namespace Personal.Models.Model.Base;

/// <summary>
/// Уровень параметра 
/// </summary>
public interface ILevel {

    /// <summary>
    /// Текущий уровень параметра
    /// </summary>
    int CurrentLevel { get; set; }

    /// <summary>
    /// Текуший уровень exp
    /// </summary>
    long Exp { get; }

    /// <summary>
    /// Сколько требуется Exp для повышения уровня
    /// </summary>
    long ExpForUpdateRequired { get; }

    /// <summary>
    /// Добвить очков опыта 
    /// </summary>
    /// <param name="exp"></param>
    void AddExp(long exp, bool updateLevel = false);

    /// <summary>
    /// Повысить уровень, если достаточно EXP
    /// </summary>
    void UpdateLevelIfPossible();

}
