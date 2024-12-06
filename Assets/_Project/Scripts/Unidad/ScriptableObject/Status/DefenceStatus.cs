using System;
using UnityEngine;

[Serializable]
public class DefenceStatus
{
    [Header("방어 수치")]
    [Range(0, int.MaxValue)] public int physicalDefence;
    [Range(0, int.MaxValue)] public int magicDefence;

    [Header("크리티컬 저항률")]
    public float physicalCriticalResistance;
    public float magicCriticalResistance;

    [Header("회피율")]
    [Range(0, 80f)] public float dodgeProbability = 5;

    [Header("속성 보너스")]
    [Range(0, 80f)] public float fireResistanceBonus = 0f;
    [Range(0, 80f)] public float waterResistanceBonus = 0f;
    [Range(0, 80f)] public float airResistanceBonus = 0f;
    [Range(0, 80f)] public float earthResistanceBonus = 0f;
    
    [Range(0, 80f)] public float lightResistanceBonus = 0f;
    [Range(0, 80f)] public float darkResistanceBonus = 0f;
}