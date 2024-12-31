using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffectBase: MonoBehaviour
{
    public UnitType TargetType
    {
        get;
    }
    public Unidad Caster
    {
        get;
        set;
    }
    public Vector2 EffectRange
    {
        get;
        set;
    }
    public float Influence
    {
        get;
        set;
    }
}