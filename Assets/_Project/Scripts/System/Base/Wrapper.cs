using System.Collections.Generic;

[System.Serializable]
public class Wrapper<T>
{
    public T[] list;

    public Wrapper() { }

    #region List
    public Wrapper(List<T> list)
    {
        if (list == null) this.list = new T[0];
        else this.list = list.ToArray();
    }

    public static implicit operator List<T>(Wrapper<T> wrapper)
    {
        if (wrapper.list == null) return default;

        return new(wrapper.list);
    }
    #endregion

    #region HashSet
    public Wrapper(HashSet<T> values) => list = new List<T>(values).ToArray();

    public static implicit operator HashSet<T>(Wrapper<T> wrapper) => new(wrapper.list);
    #endregion
}
