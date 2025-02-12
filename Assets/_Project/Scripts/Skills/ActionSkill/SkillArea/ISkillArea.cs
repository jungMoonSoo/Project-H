using UnityEngine;

public interface ISkillArea
{
    /// <summary>
    /// Drag등을 통해 포지션을 옮기는 Method
    /// </summary>
    /// <param name="transform">SkillArea의 Transform</param>
    /// <param name="caster">시전자</param>
    /// <param name="castedPosition">시전하려는 위치</param>
    /// <returns>실제 가리킬 표적</returns>
    public Vector3? SetPosition(Transform transform, Unidad caster, Vector3 castedPosition);
}