using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Tile> allyTiles;
    public List<Tile> enemyTiles;

    private TouchInfo info;
    private Unit selectUnit;

    private readonly Tile[] selectTile = new Tile[2];

    private void Start()
    {
        for (int i = 0; i < allyTiles.Count; i++) allyTiles[i].IsAlly = true;
        for (int i = 0; i < enemyTiles.Count; i++) enemyTiles[i].IsAlly = false;
    }

    private void Update()
    {
        if (UnitManager.Instance.isPlay) return;

        CheckTouch();
        DragUnit();
    }

    public void ToggleTile()
    {
        for (int i = 0; i < allyTiles.Count; i++) allyTiles[i].SetActive(!UnitManager.Instance.isPlay);
        for (int i = 0; i < enemyTiles.Count; i++) enemyTiles[i].SetActive(!UnitManager.Instance.isPlay);
    }

    private void CheckTouch()
    {
        info = TouchSystem.Instance.GetTouch(0);

        if (info.phase == TouchPhase.Began)
        {
            if (info.gameObject == null) return;

            info.gameObject.TryGetComponent(out selectTile[0]);

            if (selectTile[0] == null) return;
            if (selectTile[0].unit == null) return;
            if (!selectTile[0].IsAlly) return;

            selectUnit = selectTile[1].unit;

            selectTile[0] = selectTile[1];
            selectTile[1] = null;
        }
        else if (info.phase == TouchPhase.Ended || info.phase == TouchPhase.Canceled)
        {
            if (selectTile[0] == null) return;

            if (info.gameObject != null)
            {
                info.gameObject.TryGetComponent(out selectTile[1]);

                if (selectTile[1] != null)
                {
                    if (selectTile[1].IsAlly)
                    {
                        selectTile[0].unit = selectTile[1].unit;
                        selectTile[1].unit = selectUnit;

                        selectUnit = null;

                        if (selectTile[0].unit != null) selectTile[0].unit.SetPos(selectTile[0].transform.position);

                        selectTile[1].unit.SetPos(selectTile[1].transform.position);
                    }
                }
            }

            ReturnUnit();
        }
    }

    private void DragUnit()
    {
        if (selectUnit == null) return;

        selectUnit.transform.position = info.pos;
    }

    private void ReturnUnit()
    {
        if (selectTile[0] != null)
        {
            selectTile[0].unit?.ReturnToPos();
            selectTile[0] = null;
        }

        selectTile[1] = null;
        selectUnit = null;
    }
}
