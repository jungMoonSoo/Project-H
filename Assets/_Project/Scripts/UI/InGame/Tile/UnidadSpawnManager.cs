using System.Collections;
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
        UnidadStatus unidadStatus = UnidadManager.Instance.GetStatus(unitId);

        List<TileHandle> tiles = UnitDeployManager.Instance.GetTiles(owner);

        if (tiles[tileId].Unit != null)
        {
            if (tiles.Count > tileId + 1) return Spawn(unitId, tileId + 1, owner);

            return false;
        }

        Transform parent = tiles[tileId].Type switch
        {
            UnitType.Ally => spawnPointAlly,
            UnitType.Enemy => spawnPointEnemy,
            _ => null,
        };

        if (Instantiate(unidadStatus.unidadPrefab, parent).TryGetComponent(out Unidad unit))
        {
            tiles[tileId].Unit = unit;
            unit.Owner = owner;

            unit.statusBar = Instantiate(unidadHpBar, hpBarParent);
            unit.statusBar.Init(unit.StatusUiPosition);

            return true;
        }

        return false;
    }
}
