using System;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    private IHitableCheck hitableCheck;

    private Unidad unidad;
    private Action<Unidad> callback;

    private ObjectPool<HitObject> hitObjects;

    public void Init(ObjectPool<HitObject> hitObjects) => this.hitObjects = hitObjects;

    public void Init(Unidad unidad, Action<Unidad> callback)
    {
        if (hitableCheck == null)
        {
            this.unidad = unidad;
            this.callback = callback;

            TryGetComponent(out hitableCheck);
        }
    }

    private void Update()
    {
        if (hitableCheck.Check())
        {
            callback?.Invoke(unidad);

            hitObjects.Enqueue(this);
        }
    }
}
