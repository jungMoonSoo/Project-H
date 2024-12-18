using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandle : MonoBehaviour
{
    [SerializeField] private bool isSelectable;

    public Unidad Unit { get; private set; }
    public bool IsSelectable => isSelectable;

    public void SetUnit(Unidad _unit)
    {
        Unit = _unit;

        if (Unit != null) ReturnPos();
    }

    public void SwapUnits(TileHandle _tile)
    {
        Unidad _unit = null;

        if (_tile != null)
        {
            _unit = _tile.Unit;
            _tile.SetUnit(Unit);
        }

        SetUnit(_unit);
    }

    public void SetUnitPos(Vector2 _pos)
    {
        if (Unit != null) Unit.transform.position = _pos;
    }

    public void ReturnPos() => SetUnitPos(transform.position);

    public void SetActive(bool _value) => gameObject.SetActive(_value);
}
