using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeployManager : Singleton<UnitDeployManager>
{
    private readonly Dictionary<UnitType, List<TileHandle>> tiles = new();

    private Vector3 offset;

    private TileHandle selectedTile;
    private BoxColliderManager selectedUnit;

    [Header("설정")]
    [SerializeField] private UnitType selectableType;
    [SerializeField] private LayerMask unitLayerMask;

    void Awake()
    {
        foreach (UnitType type in Enum.GetValues(typeof(UnitType))) tiles.Add(type, new());
    }

    private void Update() => CheckTouch();

    public void RemoveSpawnUnit(uint unitId)
    {
        UnidadStatus unidadStatus = UnidadManager.Instance.GetStatus(unitId);

        foreach (List<TileHandle> tiles in tiles.Values)
        {
            foreach (TileHandle tile in tiles)
            {
                if (tile.Unit == null) continue;

                if (tile.Unit.Status == unidadStatus)
                {
                    tile.RemoveUnit();

                    return;
                }
            }
        }
    }

    public List<Unidad> GetSpawnUnits(UnitType type)
    {
        List<Unidad> units = new();

        foreach (TileHandle tile in tiles[type])
        {
            if (tile.Unit != null) units.Add(tile.Unit);
        }

        return units;
    }

    public void SetTile(TileHandle tile, bool add, UnitType type)
    {
        tiles[type].Remove(tile);

        if (add) tiles[type].Add(tile);

        tiles[type].Sort((a, b) => a.name.CompareTo(b.name));
    }

    public List<TileHandle> GetTiles(UnitType type) => tiles[type];

    public void SetAllTileActive(bool isActive)
    {
        foreach (List<TileHandle> tiles in tiles.Values)
        {
            foreach (TileHandle tile in tiles) tile.SetActive(isActive);
        }
    }

    private void CheckTouch()
    {
        TouchInfo info = TouchSystem.GetTouch(0, unitLayerMask);

        switch (info.phase)
        {
            case TouchPhase.Began:
                if (info[0] != null && info[0].TryGetComponent(out selectedUnit))
                {
                    if (selectedUnit.UnitType != selectableType) selectedUnit = null;
                    else
                    {
                        offset = selectedUnit.transform.position - info.GetSpecificYPos(0);

                        selectedUnit.TryGetHitComponent(out selectedTile, ~unitLayerMask);

                        if (selectedTile == null) selectedUnit = null;
                        else selectedUnit.PickUnit();
                    }
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (selectedUnit != null) TouchEnded();
                break;

            default:
                if (selectedUnit != null) selectedUnit.SetUnitPos(MapManager.Instance.ClampPositionToMap(info.GetSpecificYPos(0) + offset, Vector2.zero));
                break;
        }
    }

    private void TouchEnded()
    {
        if (selectedUnit.TryGetHitComponent(out TileHandle targetTile, ~unitLayerMask) && targetTile.Type == selectableType) selectedTile.SwapUnits(targetTile);
        else selectedTile.ReturnPos();

        selectedUnit.DropUnit();

        selectedUnit = null;
        selectedTile = null;
    }
}
