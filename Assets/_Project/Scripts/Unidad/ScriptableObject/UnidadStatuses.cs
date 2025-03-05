using System;
using UnityEngine;

[Serializable]
public class UnidadStatuses
{
    [Header("기본 스테이터스")]
    public NormalStatus normal;

    [Header("공격 스테이터스")]
    public AttackStatus attack;
    
    [Header("방어 스테이터스")]
    public DefenceStatus defence;
}
