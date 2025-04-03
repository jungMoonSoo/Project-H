using System.Collections.Generic;

[System.Serializable]
public class Wrapper<T>
{
    public List<T> list;

    public Wrapper() => list = new();

    #region List
    public Wrapper(List<T> list) => this.list = list;

    public static implicit operator List<T>(Wrapper<T> wrapper) => new(wrapper.list);
    #endregion

    #region HashSet
    public Wrapper(HashSet<T> values) => list = new(values);

    public static implicit operator HashSet<T>(Wrapper<T> wrapper) => new(wrapper.list);
    #endregion
}
