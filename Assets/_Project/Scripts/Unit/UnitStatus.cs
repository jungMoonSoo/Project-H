using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStatus
{
    public BindData<int>[] hp = new BindData<int>[2];
    public BindData<int>[] mp = new BindData<int>[2];

    public int mpRegen;

    public float moveSpeed;

    public int atk;
    [Range(0f, 1f)]public float atkAnimPoint;
}
