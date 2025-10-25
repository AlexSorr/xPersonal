using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal.Models.Model.Base;

/// <summary>
/// Базовый класс для всех сущностей, хранящихся в БД
/// </summary>
public abstract class Entity : IEntity {

    public Entity() { }

    /// <inheritdoc/>
    [Key]
    public Guid Id { get; set; }
    
    /// <inheritdoc/>
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    /// <inheritdoc/>
    public DateTime? ChangeDate { get; set; }

    
    public override bool Equals(object? obj) {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not Entity other) return false;
        return Id == other.Id;
    }

    public override int GetHashCode() => HashCode.Combine(Id, GetType());

    public static bool operator ==(Entity? left, Entity? right) {
        if (left is null) return right is null;
        return left.Equals(right);
    }
    
    public static bool operator !=(Entity? left, Entity? right) => !(left == right);

}