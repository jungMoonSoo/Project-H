using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public bool spawn;

    public Transform spawnPoint_Ally;
    public Transform spawnPoint_Enemy;

    public TileManager tileManager;

    private void Update()
    {
        if (!spawn) return;

        spawn = false;

        UnidadStatus _unidadStatus = UnidadManager.Instance.GetStatus(0);

        Spawn(_unidadStatus, 0, true);
    }

    private void Spawn(UnidadStatus _unidadStatus, int _tileId, bool _ally)
    {
        Unidad _unit = tileManager.allyTiles[_tileId].Unit;

        if (_unit != null)
        {
            if (tileManager.allyTiles.Count > _tileId + 1) Spawn(_unidadStatus, _tileId + 1, true);

            return;
        }

        if (_ally)
        {
            _unit = Instantiate(_unidadStatus.unidadPrefab, spawnPoint_Ally).GetComponent<Unidad>();

            tileManager.allyTiles[_tileId].SetUnit(_unit);
        }
        else
        {
            _unit = Instantiate(_unidadStatus.unidadPrefab, spawnPoint_Enemy).GetComponent<Unidad>();

            tileManager.enemyTiles[_tileId].SetUnit(_unit);
        }
    }
}
