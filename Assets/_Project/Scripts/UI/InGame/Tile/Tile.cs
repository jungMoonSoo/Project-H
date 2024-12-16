using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Unidad Unit { get; private set; }
    public bool IsSelectable { get; private set; }

    public void Init(bool _isSelectable) => IsSelectable = _isSelectable;

    public void SetUnit(Unidad _unit)
    {
        Unit = _unit;

        if (Unit != null) ReturnPos();
    }

    public void ReturnPos()
    {
        if (Unit != null) Unit.transform.position = transform.position;
    }

    public void SetActive(bool _value) => gameObject.SetActive(_value);
}
