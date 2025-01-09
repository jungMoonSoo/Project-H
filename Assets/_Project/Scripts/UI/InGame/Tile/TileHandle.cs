using UnityEngine;

public class TileHandle : MonoBehaviour
{
    [SerializeField] private UnitType type;

    private Unidad unit;
    public Unidad Unit
    {
        get => unit;
        set
        {
            unit = value;

            if (value != null) ReturnPos();
        }
    }

    public UnitType Type => type;

    public void Start() => UnitDeployManager.Instance.SetTile(this, true, type);

    public void RemoveUnit()
    {
        Destroy(Unit.gameObject);

        Unit = null;
    }

    public void SwapUnits(TileHandle tile) => (Unit, tile.Unit) = (tile.Unit, Unit);

    public void ReturnPos()
    {
        if (Unit != null) Unit.transform.position = transform.position;
    }

    public void SetActive(bool value) => gameObject.SetActive(value);
}
