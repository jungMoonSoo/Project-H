using System;
using UnityEngine;

[Serializable]
public class AttackStatus
{
    [Header("공격력")]
    [Range(0, int.MaxValue)] public int physicalDamage;
    [Range(0, int.MaxValue)] public int magicDamage;
    
    [Header("치명타 추가피해")]
    [Range(0, float.MaxValue)] public float physicalCriticalDamage;
    [Range(0, float.MaxValue)] public float magicCriticalDamage;

    [Header("치명타 확률")]
    public float physicalCriticalProbability;
    public float magicCriticalProbability;

    [Header("명중률")]
    [Range(70, 150)] public float accuracy = 100f;

    [Header("속성 보너스")]
    [Range(0, float.MaxValue)] public float fireDamageBonus;
    [Range(0, float.MaxValue)] public float waterDamageBonus;
    [Range(0, float.MaxValue)] public float airDamageBonus;
    [Range(0, float.MaxValue)] public float earthDamageBonus;
    [Range(0, float.MaxValue)] public float lightDamageBonus;
    [Range(0, float.MaxValue)] public float darkDamageBonus;
}