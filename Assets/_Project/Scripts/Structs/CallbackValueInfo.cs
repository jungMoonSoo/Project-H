using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CallbackValueInfo<T>
{
    public T type;
    public float value;

    public CallbackValueInfo(T _type, float _value)
    {
        type = _type;
        value = _value;
    }
}
