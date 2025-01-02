using UnityEngine;

public interface ISkillEffect
{
    /// <summary>
    /// 해당 스킬을 시전한 Unit
    /// </summary>
    public Unidad Caster
    {
        get;
        set;
    }
    /// <summary>
    /// 영향을 받을 유닛타입
    /// </summary>
    public UnitType TargetType
    {
        get;
    }
    /// <summary>
    /// 스킬 영향 범위
    /// </summary>
    public Vector2 EffectRange
    {
        get;
    }
    /// <summary>
    /// 스킬의 강도
    /// </summary>
    public float Influence
    {
        get;
        set;
    }

    public ITargetingSystem TargetingSystem
    {
        get;
    }

    
    /// <summary>
    /// 생성 Event Method
    /// </summary>
    public void OnCreate();
    /// <summary>
    /// 효과 적용 Event Method
    /// </summary>
    public void OnTrigger();
    /// <summary>
    /// 스킬 이펙트 종료 Method
    /// </summary>
    public void OnFinish();
    
    
    /// <summary>
    /// 스킬 목표 위치 지정 Method
    /// </summary>
    /// <param name="position">목표 위치</param>
    public void SetPosition(Vector2 position);
}