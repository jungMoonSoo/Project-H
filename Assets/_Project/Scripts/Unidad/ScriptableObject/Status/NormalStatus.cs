using System;
using UnityEngine;

[Serializable]
public class NormalStatus
{
    public int maxHp = 100;
    public int maxMp = 100;
    [Range(0.25f, float.MaxValue)] public float moveSpeed;
    [Range(0.5f, 5f)] public float attackSpeed;
}
