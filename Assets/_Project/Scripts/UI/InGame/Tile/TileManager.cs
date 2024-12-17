using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Tile> allyTiles;
    public List<Tile> enemyTiles;

    private TouchInfo info;
    private Unidad selectedUnit;

    private Tile selectedTile;
    private Tile targetTile;

    private void Start()
    {
        for (int i = 0; i < allyTiles.Count; i++) allyTiles[i].Init(true);
        for (int i = 0; i < enemyTiles.Count; i++) enemyTiles[i].Init(false);
    }

    private void Update()
    {
        // 시작 여부 확인 필요

        CheckTouch();
        DragUnit();
    }

    public void ToggleTile()
    {
        SetTileActiveState(false);
    }

    private void SetTileActiveState(bool _isActive)
    {
        for (int i = 0; i < allyTiles.Count; i++) allyTiles[i].SetActive(_isActive);
        for (int i = 0; i < enemyTiles.Count; i++) enemyTiles[i].SetActive(_isActive);
    }

    private void CheckTouch()
    {
        info = TouchSystem.Instance.GetTouch(0);

        if (info.phase == TouchPhase.Began) HandleTouchBegan();
        else if (info.phase == TouchPhase.Ended || info.phase == TouchPhase.Canceled) HandleTouchEnded();
        else HandleTouchMoved();
    }

    private void HandleTouchBegan()
    {
        if (info.gameObject == null) return;

        info.gameObject.TryGetComponent(out selectedTile);

        if (selectedTile == null || selectedTile.Unit == null || !selectedTile.IsSelectable) return;

        selectedUnit = selectedTile.Unit;
    }

    private void HandleTouchMoved()
    {
        if (selectedUnit == null) return;
        if (info.gameObject == null) return;
        if (targetTile != null && info.gameObject == targetTile.gameObject) return;

        info.gameObject.TryGetComponent(out targetTile);
    }

    private void HandleTouchEnded()
    {
        if (targetTile != null && targetTile.IsSelectable) SwapUnits();

        ReturnUnit();
    }

    private void SwapUnits()
    {
        selectedTile.SetUnit(targetTile.Unit);
        targetTile.SetUnit(selectedUnit);

        selectedUnit = null;
    }

    // 유닛 드래그
    private void DragUnit()
    {
        if (selectedUnit == null) return;

        selectedUnit.transform.position = info.pos;
    }

    // 유닛 원위치 반환
    private void ReturnUnit()
    {
        if (selectedTile != null)
        {
            selectedTile.ReturnPos();
            selectedTile = null;
        }

        targetTile = null;
        selectedUnit = null;
    }
}
