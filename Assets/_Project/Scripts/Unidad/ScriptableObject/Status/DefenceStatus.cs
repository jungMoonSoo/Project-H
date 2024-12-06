using System;
using UnityEngine;

[Serializable]
public class DefenceStatus
{
    public float physicalDefence;
    public float magicDefence;

    public float physicalCriticalResistance;
    public float magicCriticalResistance;

    public float dodgeProbability;

    [Header("속성 보너스")]
    public float fireResistanceBonus;
    public float waterResistanceBonus;
    public float airResistanceBonus;
    public float earthResistanceBonus;
    public float lightResistanceBonus;
    public float darkResistanceBonus;
}