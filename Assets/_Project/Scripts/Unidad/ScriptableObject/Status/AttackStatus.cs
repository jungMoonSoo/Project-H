using System;
using UnityEngine;

[Serializable]
public class AttackStatus
{
    public float physicalDamage;
    public float magicDamage;
    
    public float physicalCriticalDamage;
    public float magicCriticalDamage;

    public float physicalCriticalProbability;
    public float magicCriticalProbability;

    public float accuracy;

    [Header("속성 보너스")]
    public float fireDamageBonus;
    public float waterDamageBonus;
    public float airDamageBonus;
    public float earthDamageBonus;
    public float lightDamageBonus;
    public float darkDamageBonus;
}