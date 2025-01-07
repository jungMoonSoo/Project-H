using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unidad/Status", fileName = "NewUnidadStatus")]
public class UnidadStatus : ScriptableObject
{
    public uint id;
        
    [Header("Prefabs")]
    public GameObject unidadPrefab;
    
    [Header("Skill Info")]
    public ActionSkillInfo skillInfo;
    
    [Header("기본 스테이터스")]
    public NormalStatus[] normalStatus;

    [Header("공격 스테이터스")]
    public AttackStatus[] attackStatus;
    
    [Header("방어 스테이터스")]
    public DefenceStatus[] defenceStatus;
}
