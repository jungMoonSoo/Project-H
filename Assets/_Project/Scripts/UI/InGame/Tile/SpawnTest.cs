using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public bool spawn;

    public Transform spawnPoint_Ally;
    public Transform spawnPoint_Enemy;

    public TileManager tileManager;

    public UnidadStatusBar unidadHpBar;
    public Transform hpBarParent;

    private void Update()
    {
        if (!spawn) return;

        spawn = false;

        UnidadStatus _unidadStatus = UnidadManager.Instance.GetStatus(0);

        Spawn(_unidadStatus, 0, true);
    }

    public void Spawn(UnidadStatus _unidadStatus, int _tileId, bool _ally)
    {
        if (_ally) Spawn(_unidadStatus, _tileId, tileManager.allyTiles, spawnPoint_Ally);
        else Spawn(_unidadStatus, _tileId, tileManager.enemyTiles, spawnPoint_Enemy);
    }

    private void Spawn(UnidadStatus _unidadStatus, int _tileId, List<TileHandle> _tiles, Transform _parent)
    {
        if (_tiles[_tileId].Unit != null)
        {
            if (_tiles.Count > _tileId + 1) Spawn(_unidadStatus, _tileId + 1, _tiles, _parent);

            return;
        }

        Unidad _unit = Instantiate(_unidadStatus.unidadPrefab, _parent).GetComponent<Unidad>();
        UnidadStatusBar _hpBar = Instantiate(unidadHpBar, hpBarParent);

        _tiles[_tileId].SetUnit(_unit);
        _hpBar.Init(_unit.transform);
    }
}
