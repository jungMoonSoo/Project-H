using System;

[Serializable]
public class DoubleValue<T, U>
{
    public T first;
    public U second;

    public DoubleValue()
    {
        first = default;
        second = default;
    }

    public DoubleValue(T first, U second)
    {
        this.first = first;
        this.second = second;
    }

    public void Set(T first, U second)
    {
        this.first = first;
        this.second = second;
    }
}
