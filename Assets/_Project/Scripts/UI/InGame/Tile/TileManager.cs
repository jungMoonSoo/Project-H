using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<TileHandle> allyTiles;
    [SerializeField] private List<TileHandle> enemyTiles;

    private Vector2 offsetPos;

    private TileHandle selectedTile;
    private UnidadColliderHandle selectedUnit;

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
                if (info.gameObject == null) return;

                if (info.gameObject.TryGetComponent(out selectedUnit))
                {
                    if (selectedUnit.UnitType != UnitType.Ally) selectedUnit = null;
                    else
                    {
                        offsetPos = (Vector2)selectedUnit.transform.position - info.pos;
                        selectedTile = selectedUnit.GetHitComponent<TileHandle>();
                    }
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (selectedUnit != null) TouchEnded();
                break;

            default:
                if (selectedUnit != null) selectedUnit.SetUnitPos(offsetPos + info.pos);
                break;
        }
    }

    private void TouchEnded()
    {
        TileHandle targetTile = selectedUnit.GetHitComponent<TileHandle>();

        if (targetTile != null && targetTile.IsSelectable) selectedTile.SwapUnits(targetTile);
        else selectedTile.ReturnPos();

        selectedUnit = null;
        selectedTile = null;
    }
}
