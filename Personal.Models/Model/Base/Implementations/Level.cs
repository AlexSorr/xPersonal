using System;

namespace Personal.Models.Model.Base;

/// <inheritdoc/>
public class Level : ILevel {

    /// <inheritdoc/>
    public int CurrentLevel { get; set; } = 1;

    private long _exp = 0;
    /// <inheritdoc/>
    public long Exp { get => _exp; }

    private long _expForUpdateRequired = 0;
    /// <inheritdoc/>
    public long ExpForUpdateRequired { get => _expForUpdateRequired; }

    /// <inheritdoc/>
    public void AddExp(long exp, bool updateLevel = false) {
        if (exp < 0)
            throw new ArgumentOutOfRangeException("Уровню нельзя добавить отрицательное значение Exp");
        _exp += exp;
        if (updateLevel)
            UpdateLevelIfPossible();
    }

    /// <inheritdoc/>
    public void UpdateLevelIfPossible() {
        while (Exp >= ExpForUpdateRequired) {
            CurrentLevel++;
            CalculateNewRequiredExp();
        }
    }

    /// <summary>
    /// Рассчитать новое необходимое количество Exp после повышения уровня
    /// TODO продумать логику расчета 
    /// </summary>
    private void CalculateNewRequiredExp() => _expForUpdateRequired *= CurrentLevel;
    
}
