using System;
using UnityEngine;

public class LookController : Singleton<LookController>
{
    public Action<Vector3> actions;

    public Vector3 Rotation { get; private set; } = Vector3.zero;

    private void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;

        if (eulerAngles == Rotation) return;

        actions?.Invoke(eulerAngles - Rotation);

        Rotation = eulerAngles;
    }
}
