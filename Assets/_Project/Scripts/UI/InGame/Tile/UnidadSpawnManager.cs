using System;
using System.Collections.Generic;
using UnityEngine;

public class UnidadSpawnManager : MonoBehaviour
{
    [Header("테스트")]
    [SerializeField] private bool spawnAlly;
    [SerializeField] private bool spawnEnemy;

    [Header("스폰 정보")]
    [SerializeField] private Transform spawnPointAlly;
    [SerializeField] private Transform spawnPointEnemy;

    [Header("스테이터스 바")]
    [SerializeField] private UnidadStatusBar unidadHpBar;
    [SerializeField] private Transform hpBarParent;

    private void Update()
    {
        if (spawnAlly)
        {
            spawnAlly = false;

            Spawn(0, 0, UnitType.Ally);
        }

        if (spawnEnemy)
        {
            spawnEnemy = false;

            Spawn(0, 0, UnitType.Enemy);
        }
    }

    public bool Spawn(uint unitId, UnitType owner = UnitType.Ally) => Spawn(unitId, 0, owner);

    public bool Spawn(uint unitId, int tileId, UnitType owner)
    {
        List<TileHandle> tiles = UnitDeployManager.Instance.GetTiles(owner);

        if (tiles[tileId].Unit != null)
        {
            if (tiles.Count > tileId + 1) return Spawn(unitId, tileId + 1, owner);

            return false;
        }

        tiles[tileId].Unit = Spawn(unitId, tiles[tileId].transform.position, owner);

        return tiles[tileId].Unit != null;
    }

    public Unidad Spawn(uint unitId, Vector2 pos, UnitType owner)
    {
        UnidadStatus unidadStatus = UnidadManager.Instance.GetStatus(unitId);

        Transform parent = owner switch
        {
            UnitType.Ally => spawnPointAlly,
            UnitType.Enemy => spawnPointEnemy,
            _ => null,
        };

        if (Instantiate(unidadStatus.unidadPrefab, parent).TryGetComponent(out Unidad unit))
        {
            unit.Owner = owner;
            unit.transform.position = pos;

            unit.Init();

            UnidadStatusBar statusBar = Instantiate(unidadHpBar, hpBarParent);

            unit.SetStatusBar(statusBar);
            statusBar.Init(unit.StatusUiPosition);

            return unit;
        }

        return null;
    }
}
