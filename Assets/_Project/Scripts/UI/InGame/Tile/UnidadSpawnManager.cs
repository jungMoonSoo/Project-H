using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadSpawnManager : MonoBehaviour
{
    [Header("테스트")]
    public bool spawnAlly;
    public bool spawnEnemy;

    [Header("스폰 정보")]
    public Transform spawnPoint_Ally;
    public Transform spawnPoint_Enemy;

    public TileManager tileManager;

    [Header("스테이터스 바")]
    public UnidadStatusBar unidadHpBar;
    public Transform hpBarParent;

    private void Update()
    {
        if (spawnAlly)
        {
            spawnAlly = false;

            UnidadStatus _unidadStatus = UnidadManager.Instance.GetStatus(0);

            Spawn(_unidadStatus, 0, true);
        }

        if (spawnEnemy)
        {
            spawnEnemy = false;

            UnidadStatus _unidadStatus = UnidadManager.Instance.GetStatus(0);

            Spawn(_unidadStatus, 0, false);
        }
    }

    public void SpawnAllyUnit(uint _unitId) => Spawn(UnidadManager.Instance.GetStatus(_unitId), 0, true);

    public void Spawn(UnidadStatus _unidadStatus, int _tileId, bool _ally)
    {
        Unidad _unit = _ally ?
            Spawn(_unidadStatus, _tileId, tileManager.allyTiles, spawnPoint_Ally) :
            Spawn(_unidadStatus, _tileId, tileManager.enemyTiles, spawnPoint_Enemy);

        if (_unit != null)
        {
            _unit.Owner = _ally ? UnitType.Ally : UnitType.Enemy;

            UnidadStatusBar _hpBar = Instantiate(unidadHpBar, hpBarParent);

            _hpBar.Init(_unit.StatusUiPosition);

            _unit.statusBar = _hpBar;
        }
    }

    private Unidad Spawn(UnidadStatus _unidadStatus, int _tileId, List<TileHandle> _tiles, Transform _parent)
    {
        if (_tiles[_tileId].Unit != null)
        {
            if (_tiles.Count > _tileId + 1) return Spawn(_unidadStatus, _tileId + 1, _tiles, _parent);

            return null;
        }

        Unidad _unit = Instantiate(_unidadStatus.unidadPrefab, _parent).GetComponent<Unidad>();

        _tiles[_tileId].SetUnit(_unit);

        return _unit;
    }
}
