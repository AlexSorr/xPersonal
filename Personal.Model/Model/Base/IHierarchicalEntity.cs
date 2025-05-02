namespace Personal.Model.Base;

public interface IHierarchicalEntity<T> where T : class, IEntity {

    T? Parent { get; set; }

}
