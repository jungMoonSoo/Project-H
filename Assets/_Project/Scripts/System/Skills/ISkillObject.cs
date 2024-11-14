using System.Collections;
using UnityEngine;

public interface ISkillObject
{
    public Vector2 EffectRange
    {
        get;
    }

    /// <summary>
    /// 스킬 효과 적용 Method
    /// </summary>
    public void ApplyEffect();
}