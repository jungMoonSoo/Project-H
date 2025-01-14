using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillInfoBase : ScriptableObject
{
    [Header("Skill Info")]
    [SerializeField] public uint code;
    [SerializeField] public string skillName;
    [SerializeField] public Sprite sprite;
}
