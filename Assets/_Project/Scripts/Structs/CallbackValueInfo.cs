using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CallbackValueInfo<T>
{
    public T type;
    public int value;

    public CallbackValueInfo(T _type, int _value)
    {
        type = _type;
        value = _value;
    }
}
