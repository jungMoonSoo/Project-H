using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandle : MonoBehaviour
{
    [SerializeField] private bool isSelectable;

    public Unidad Unit { get; private set; }
    public bool IsSelectable => isSelectable;

    public void SetUnit(Unidad unit)
    {
        Unit = unit;

        if (Unit != null) ReturnPos();
    }

    public void SwapUnits(TileHandle tile)
    {
        Unidad unit = null;

        if (tile != null)
        {
            unit = tile.Unit;
            tile.SetUnit(Unit);
        }

        SetUnit(unit);
    }

    public void SetUnitPos(Vector2 pos)
    {
        if (Unit != null) Unit.transform.position = pos;
    }

    public void ReturnPos() => SetUnitPos(transform.position);

    public void SetActive(bool value) => gameObject.SetActive(value);
}
