using UnityEngine;

public interface ISkill
{
    /// <summary>
    /// 스킬 사용 오브젝트
    /// </summary>
    public GameObject SkillEffect
    {
        get;
    }
    /// <summary>
    /// 스킬 실제 동작 부분
    /// </summary>
    public void Execute();
}