using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<TileHandle> allyTiles;
    [SerializeField] private List<TileHandle> enemyTiles;

    private TileHandle selectedTile;
    private TileHandle targetTile;

    public List<TileHandle> AllyTiles => allyTiles;
    public List<TileHandle> EnemyTiles => enemyTiles;

    private void Update()
    {
        // 시작 여부 확인 필요

        CheckTouch();
    }

    public void ToggleTile() => SetTileActiveState(false);

    private void SetTileActiveState(bool isActive)
    {
        for (int i = 0; i < AllyTiles.Count; i++) AllyTiles[i].SetActive(isActive);
        for (int i = 0; i < EnemyTiles.Count; i++) EnemyTiles[i].SetActive(isActive);
    }

    private void CheckTouch()
    {
        TouchInfo info = TouchSystem.Instance.GetTouch(0);

        switch (info.phase)
        {
            case TouchPhase.Began:
                selectedTile = GetTile(info.gameObject);

                if (selectedTile != null && selectedTile.Unit == null) selectedTile = null;
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (selectedTile != null) TouchEnded();
                break;

            default:
                targetTile = GetTile(info.gameObject);

                if (selectedTile != null) selectedTile.SetUnitPos(info.pos);
                break;
        }
    }

    private TileHandle GetTile(GameObject target)
    {
        if (target == null) return null;

        target.TryGetComponent(out TileHandle tile);

        if (tile != null && !tile.IsSelectable) tile = null;

        return tile;
    }

    private void TouchEnded()
    {
        if (targetTile != null) selectedTile.SwapUnits(targetTile);
        else selectedTile.ReturnPos();

        selectedTile = null;
        targetTile = null;
    }
}
