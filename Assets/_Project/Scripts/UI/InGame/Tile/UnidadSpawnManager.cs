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

    [SerializeField] private UnitDeployManager unitDeployManager;

    [Header("스테이터스 바")]
    [SerializeField] private UnidadStatusBar unidadHpBar;
    [SerializeField] private Transform hpBarParent;

    private void Update()
    {
        if (spawnAlly)
        {
            spawnAlly = false;

            UnidadStatus unidadStatus = UnidadManager.Instance.GetStatus(0);

            Spawn(unidadStatus, 0, true);
        }

        if (spawnEnemy)
        {
            spawnEnemy = false;

            UnidadStatus unidadStatus = UnidadManager.Instance.GetStatus(0);

            Spawn(unidadStatus, 0, false);
        }
    }

    public bool SpawnAllyUnit(uint unitId) => Spawn(UnidadManager.Instance.GetStatus(unitId), 0, true);

    public bool Spawn(UnidadStatus unidadStatus, int tileId, bool ally)
    {
        Unidad unit = ally ?
            Spawn(unidadStatus, tileId, unitDeployManager.AllyTiles, spawnPointAlly) :
            Spawn(unidadStatus, tileId, unitDeployManager.EnemyTiles, spawnPointEnemy);

        if (unit != null)
        {
            unit.Owner = ally ? UnitType.Ally : UnitType.Enemy;

            UnidadStatusBar hpBar = Instantiate(unidadHpBar, hpBarParent);

            hpBar.Init(unit.StatusUiPosition);

            unit.StatusBar = hpBar;

            return true;
        }

        return false;
    }

    private Unidad Spawn(UnidadStatus unidadStatus, int tileId, List<TileHandle> tiles, Transform parent)
    {
        if (tiles[tileId].Unit != null)
        {
            if (tiles.Count > tileId + 1) return Spawn(unidadStatus, tileId + 1, tiles, parent);

            return null;
        }

        Unidad unit = Instantiate(unidadStatus.unidadPrefab, parent).GetComponent<Unidad>();

        tiles[tileId].SetUnit(unit);

        return unit;
    }
}
