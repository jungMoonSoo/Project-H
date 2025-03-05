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

    [Header("Status Info")]
    public UnidadStatuses[] statuses;
}
