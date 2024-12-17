using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unidad/Status", fileName = "NewUnidadStatus")]
public class UnidadStatus : ScriptableObject
{
    public uint id;
        
    [Header("Prefabs")]
    public GameObject unidadPrefab;
    public GameObject skillPrefab;
    
    [Header("기본 스테이터스")]
    public int maxHp = 100;
    public int maxMp = 100;
    [Range(0.25f, float.MaxValue)] public float moveSpeed;
    [Range(0.5f, 5f)] public float attackSpeed;

    [Header("공격 스테이터스")]
    public AttackStatus attackStatus;
    
    [Header("방어 스테이터스")]
    public DefenceStatus defenceStatus;
}
