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

    public int atk; // 공격력
    [Range(0f, 1f)]public float atkAnimPoint; // 애니메이션 공격 적용 지점

    public int acc; // 명중률
    public int crp; // 치명타 피해
    public int cri; // 치명타 확률
    public int skp; // 스킬 계수
    public int edi; // 추가 피해율

    public int def; // 방어력
    public int dod; // 회피율
    public int ca; // 치명타 저항률
}
