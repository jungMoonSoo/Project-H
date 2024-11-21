using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Unit Unit { get; private set; }
    public bool IsAlly { get; private set; }

    public void Init(bool _isAlly)
    {
        IsAlly = _isAlly;
    }

    public void SetUnit(Unit _unit)
    {
        Unit = _unit;

        if (_unit == null) return;

        _unit.SetPos(transform.position);
    }

    public void ReturnPos()
    {
        if (Unit == null) return;

        Unit.ReturnToPos();
    }

    public void SetActive(bool _active)
    {
        gameObject.SetActive(_active);
    }
}
