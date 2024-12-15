using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Object
{
    public System.Action<T> OnDequeue { private get; set; }
    public System.Action<T> OnEnqueue { private get; set; }

    private readonly T poolObject;

    private readonly Queue<T> poolObjects = new();

    public ObjectPool(T _poolObject) => poolObject = _poolObject;

    public T Dequeue(Transform _parent = null)
    {
        T _object = poolObjects.Count == 0 ? Object.Instantiate(poolObject, _parent) : poolObjects.Dequeue();

        OnDequeue?.Invoke(_object);

        return _object;
    }

    public void Enqueue(T _object)
    {
        if (_object == null) return;

        OnEnqueue?.Invoke(_object);

        poolObjects.Enqueue(_object);
    }
}
