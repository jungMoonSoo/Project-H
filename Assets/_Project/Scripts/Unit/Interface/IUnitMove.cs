using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitMove
{
    public Unit Target { get; }
    public Vector3 MovePos { get; }

    public EllipseCollider EllipseCollider { get; }
}
