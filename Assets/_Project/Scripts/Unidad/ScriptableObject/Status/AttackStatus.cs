using System;
using UnityEngine;

[Serializable]
public class AttackStatus
{
    [Header("공격력")]
    public int physicalDamage;
    public int magicDamage;
    
    [Header("치명타 추가피해")]
    public float physicalCriticalDamage;
    public float magicCriticalDamage;

    [Header("치명타 확률")]
    public float physicalCriticalProbability;
    public float magicCriticalProbability;

    [Header("명중률")]
    [Range(70, 150)] public float accuracy = 100f;

    [Header("속성 보너스")]
    public float fireDamageBonus;
    public float waterDamageBonus;
    public float airDamageBonus;
    public float earthDamageBonus;
    public float lightDamageBonus;
    public float darkDamageBonus;
}