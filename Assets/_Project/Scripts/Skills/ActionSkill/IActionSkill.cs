using UnityEngine;

public interface IActionSkill
{
    /// <summary>
    /// 스킬 사용시 생성될 SkillEffect의 Code
    /// </summary>
    public uint EffectCode { get; set; }

    /// <summary>
    /// 스킬을 사용하는 Unidad 객체<br/>
    /// 생성된 인스턴스 객체여야 함.
    /// </summary>
    public Unidad Caster { get; set; }

    
    /// <summary>
    /// 스킬 사용 위치 표시용 이펙트 오브젝트
    /// </summary>
    public ISkillArea SkillArea { get; set; }
    /// <summary>
    /// 스킬 범위 크기
    /// </summary>
    public Vector2 AreaSize { get; set; }
    /// <summary>
    /// 스킬 최대 사거리
    /// </summary>
    public Vector2 SkillRange { get; }


    /// <summary>
    /// 스킬 사용을 위해 선택받았을 때 동작 Method
    /// </summary>
    public void OnSelect();
    /// <summary>
    /// 스킬 사용을 위한 드래그 Method
    /// </summary>
    /// <param name="worldPosition">스킬이 드래그 되고 있는 실제 위치</param>
    public void OnDrag(Vector3 worldPosition);
    /// <summary>
    /// 스킬 사용이 종료된 뒤 동작 Method
    /// </summary>
    public void EndAction();


    /// <summary>
    /// 스킬 실제 동작
    /// </summary>
    /// <param name="worldPosition">스킬이 사용될 실제 위치</param>
    public void ApplyAction(Vector3 worldPosition);
}