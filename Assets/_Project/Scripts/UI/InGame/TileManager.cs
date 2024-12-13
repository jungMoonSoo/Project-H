using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Tile> allyTiles;
    public List<Tile> enemyTiles;

    private TouchInfo info;
    private Unidad selectedUnit;

    private bool storage;

    private Tile selectedTile;
    private Tile targetTile;

    private void Start()
    {
        for (int i = 0; i < allyTiles.Count; i++) allyTiles[i].Init(true);
        for (int i = 0; i < enemyTiles.Count; i++) enemyTiles[i].Init(false);
    }

    private void Update()
    {
        // if (StageManager.Instance.isPlay) return;
        // 
        // CheckTouch();
        // DragUnit();
    }

    public void ToggleTile()
    {
        SetTileActiveState(false);
    }

    public void SelectStorageUnit(Vector2 _pos)
    {
        // _pos위치에 유닛 생성 필요
        Unidad _unit = new();

        selectedUnit = _unit;
        storage = true;
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

        if (selectedTile == null || selectedTile.Unit == null || !selectedTile.IsAlly) return;

        selectedUnit = selectedTile.Unit;
    }

    // 터치 종료 처리
    private void HandleTouchEnded()
    {
        if (selectedUnit == null) return;

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
        if (storage)
        {
            // 유닛 제거 및 보관함에 유닛 이미지 생성 필요

            targetTile.Unit.gameObject.SetActive(false);

            storage = false;
        }
        else selectedTile.SetUnit(targetTile.Unit);

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
