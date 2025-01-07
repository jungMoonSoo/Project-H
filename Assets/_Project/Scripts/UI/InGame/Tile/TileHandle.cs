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

    public void RemoveUnit()
    {
        Destroy(Unit.gameObject);
        SetUnit(null);
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

    public void ReturnPos()
    {
        if (Unit != null) Unit.transform.position = transform.position;
    }

    public void SetActive(bool value) => gameObject.SetActive(value);
}
