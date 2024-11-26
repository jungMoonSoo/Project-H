using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStatus
{
    public BindData<int>[] hp = new BindData<int>[2]; // 체력
    public BindData<int>[] mp = new BindData<int>[2]; // 마력

    public int mpRegen; // 마나 회복 계수

    public float moveSpeed; // 이동 속도

    [Range(0f, 1f)]public float atkAnimPoint; // 애니메이션 공격 적용 지점

    public DamageStatus damageStatus;
    public GuardStatus guardStatus;
}
