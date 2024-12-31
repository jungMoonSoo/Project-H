using UnityEngine;

public interface ISkillArea
{
    /// <summary>
    /// Drag등을 통해 포지션을 옮기는 Method
    /// </summary>
    /// <param name="transform">SkillArea의 Transform</param>
    /// <param name="targetType">타겟 타입</param>
    /// <param name="caster">시전자</param>
    /// <param name="castedPosition">시전하려는 위치</param>
    public void SetPosition(Transform transform, TargetType targetType, Unidad caster, Vector2 castedPosition);
}