using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStatus
{
    public BindData<int>[] hp = new BindData<int>[2];
    public BindData<int>[] mp = new BindData<int>[2];

    public BindData<float> moveSpeed = new();

    public BindData<int> atk = new();
}
