using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Unit Status")]
public class UnitStatus : ScriptableObject
{
    public int maxHp;
    public int maxMp;

    public int mpRegen; // 마나 회복 계수

    public float moveSpeed; // 이동 속도

    [Range(0f, 1f)]public float atkAnimPoint; // 애니메이션 공격 적용 지점

    public DamageStatus damageStatus;
    public GuardStatus guardStatus;
}