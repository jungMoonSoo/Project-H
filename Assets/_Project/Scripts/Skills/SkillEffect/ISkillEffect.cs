using UnityEngine;

public interface ISkillEffect
{
    public Vector2 EffectRange
    {
        get;
    }
    public float Influence
    {
        get;
        set;
    }

    /// <summary>
    /// 스킬 효과 적용 Method
    /// </summary>
    public void ApplyEffect();
    public void SetPosition(Vector2 position);
    public Unidad[] GetTargets();
}