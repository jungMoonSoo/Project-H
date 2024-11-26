using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPosManager : IUnitPos
{
    private Vector3 existingPos;

    private readonly UnitType unitType;
    private readonly Transform unitTrans;

    public UnitPosManager(Transform _transform, UnitType _unitType)
    {
        unitType = _unitType;
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

        switch (unitType)
        {
            case UnitType.Ally:
                unitTrans.rotation = Quaternion.Euler(0, 180, 0);
                break;

            case UnitType.Enemy:
                unitTrans.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }
}