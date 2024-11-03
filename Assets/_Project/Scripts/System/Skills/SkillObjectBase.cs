using UnityEngine;

public abstract class SkillObjectBase: MonoBehaviour, ISkillObject
{
    protected abstract float Influence
    {
        get;
    }

    public abstract void ApplyEffect();
}