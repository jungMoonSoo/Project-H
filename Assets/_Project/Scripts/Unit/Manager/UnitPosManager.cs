using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPosManager : IUnitPos
{
    private Vector3 existingPos;

    private readonly bool isAlly;
    private readonly Transform unitTrans;

    public UnitPosManager(Transform _transform, bool _isAlly)
    {
        isAlly = _isAlly;
        unitTrans = _transform;
    }

    public Vector3 ExistingPos => existingPos;

    public void SetPos(Vector2 _pos)
    {
        existingPos = _pos;
        ReturnToPos();
    }

    public void ReturnToPos()
    {
        unitTrans.position = existingPos;
        unitTrans.rotation = isAlly ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }
}