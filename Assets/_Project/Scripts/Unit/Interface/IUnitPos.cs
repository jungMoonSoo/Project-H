using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitPos
{
    public Vector3 ExistingPos { get; }

    public void SetPos(Vector2 _pos);
    public void ReturnToPos();
}
