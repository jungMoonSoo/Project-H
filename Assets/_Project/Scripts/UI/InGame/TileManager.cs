using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Tile> allyTiles;
    public List<Tile> enemyTiles;

    private TouchInfo info;
    private Unit selectedUnit;

    private Tile selectedTile;
    private Tile targetTile;

    private void Start()
    {
        for (int i = 0; i < allyTiles.Count; i++) allyTiles[i].Init(true);
        for (int i = 0; i < enemyTiles.Count; i++) enemyTiles[i].Init(false);
    }

    private void Update()
    {
        if (UnitManager.Instance.isPlay) return;

        CheckTouch();
        DragUnit();
    }

    public void ToggleTile()
    {
        SetTileActiveState(!UnitManager.Instance.isPlay);
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
    }

    private void HandleTouchBegan()
    {
        if (info.gameObject == null) return;

        info.gameObject.TryGetComponent(out selectedTile);

        if (selectedTile == null || selectedTile.unit == null || !selectedTile.IsAlly) return;

        selectedUnit = selectedTile.unit;
        selectedTile = null;
    }

    // 터치 종료 처리
    private void HandleTouchEnded()
    {
        if (selectedTile == null) return;

        if (info.gameObject != null)
        {
            info.gameObject.TryGetComponent(out targetTile);

            if (targetTile != null && targetTile.IsAlly) SwapUnits();
        }

        ReturnUnit();
    }

    // 유닛 교환 처리
    private void SwapUnits()
    {
        selectedTile.unit = targetTile.unit;
        targetTile.unit = selectedUnit;

        selectedUnit = null;

        selectedTile.unit?.SetPos(selectedTile.transform.position);
        targetTile.unit?.SetPos(targetTile.transform.position);
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
            selectedTile.unit?.ReturnToPos();
            selectedTile = null;
        }

        targetTile = null;
        selectedUnit = null;
    }
}
