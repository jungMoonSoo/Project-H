using System;
using UnityEngine;

[Serializable]
public class DefenceStatus
{
    [Header("방어 수치")]
    public int physicalDefence;
    public int magicDefence;

    [Header("크리티컬 저항률")]
    public float physicalCriticalResistance;
    public float magicCriticalResistance;

    [Header("회피율")]
    public float dodgeProbability = 5;

    [Header("속성 보너스")]
    public float fireResistanceBonus = 0f;
    public float waterResistanceBonus = 0f;
    public float airResistanceBonus = 0f;
    public float earthResistanceBonus = 0f;
    public float lightResistanceBonus = 0f;
    public float darkResistanceBonus = 0f;
}