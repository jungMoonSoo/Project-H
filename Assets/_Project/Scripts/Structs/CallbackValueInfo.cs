public struct CallbackValueInfo<T>
{
    public T type;
    public float value;

    public CallbackValueInfo(T type, float value)
    {
        this.type = type;
        this.value = value;
    }
}
