using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageStatus
{
    public int atk; // 공격력
    public int acc; // 명중률
    public int crp; // 치명타 피해
    public int cri; // 치명타 확률
    public int skp; // 스킬 계수
    public int edi; // 추가 피해율

    public int def; // 방어력
    public int dod; // 회피율
    public int ca; // 치명타 저항률
}
