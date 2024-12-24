using System;
using UnityEngine;

[Serializable]
public class BindData<T>
{
    [SerializeField] private T value;

    private BindCallback callback;

    public T Value
    {
        get => value;
        set
        {
            if (callback == null) this.value = value;
            else callback(ref this.value, value);
        }
    }

    public void SetCallback(BindCallback callback, bool add = false)
    {
        if (add) this.callback += callback;
        else this.callback = callback;

        this.callback(ref value, value);
    }

    public delegate void BindCallback(ref T currentValue, T newValue);
}
