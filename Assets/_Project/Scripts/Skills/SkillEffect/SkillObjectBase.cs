using System.Collections.Generic;
using UnityEngine;

public abstract class SkillObjectBase: MonoBehaviour, ISkillObject
{
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
    protected virtual Unidad[] Targets => GetTargets();


    public abstract void ApplyEffect();
    public abstract void SetPosition(Vector2 position);
    public abstract Unidad[] GetTargets();
}