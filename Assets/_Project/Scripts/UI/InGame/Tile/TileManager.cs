using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<TileHandle> allyTiles;
    public List<TileHandle> enemyTiles;

    private TileHandle selectedTile;
    private TileHandle targetTile;

    private void Update()
    {
        // 시작 여부 확인 필요

        CheckTouch();
    }

    public void ToggleTile() => SetTileActiveState(false);

    private void SetTileActiveState(bool _isActive)
    {
        for (int i = 0; i < allyTiles.Count; i++) allyTiles[i].SetActive(_isActive);
        for (int i = 0; i < enemyTiles.Count; i++) enemyTiles[i].SetActive(_isActive);
    }

    private void CheckTouch()
    {
        TouchInfo _info = TouchSystem.Instance.GetTouch(0);

        switch (_info.phase)
        {
            case TouchPhase.Began:
                selectedTile = GetTile(_info.gameObject);
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (selectedTile != null) TouchEnded();
                break;

            default:
                targetTile = GetTile(_info.gameObject);

                if (selectedTile != null) selectedTile.SetUnitPos(_info.pos);
                break;
        }
    }

    private TileHandle GetTile(GameObject _target)
    {
        if (_target == null) return null;

        _target.TryGetComponent(out TileHandle _tile);

        if (_tile != null && !_tile.IsSelectable) _tile = null;

        return _tile;
    }

    private void TouchEnded()
    {
        if (targetTile != null) selectedTile.SwapUnits(targetTile);
        else selectedTile.ReturnPos();

        selectedTile = null;
        targetTile = null;
    }
}
