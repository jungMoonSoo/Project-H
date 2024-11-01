using UnityEngine;

public interface IPlayerSkill: ISkill
{
    /// <summary>
    /// 스킬 드래그 위치 표시용 이펙트 오브젝트
    /// </summary>
    public GameObject AreaEffect
    {
        get;
    }

    /// <summary>
    /// 스킬 쿨다운 시간
    /// </summary>
    public float CoolDown
    {
        get;
    }
    /// <summary>
    /// 현재 쿨다운 여부
    /// </summary>
    public bool IsCooled
    {
        get;
    }

    /// <summary>
    /// 스킬 사용을 위해 선택받았을 때 동작 Method
    /// </summary>
    public void OnSelect();
    /// <summary>
    /// 스킬 사용을 위한 드래그 Method
    /// </summary>
    /// <param name="position">터치가 된 MainCamera 기준 위치</param>
    public void OnDrag(Vector3 position);
    /// <summary>
    /// 스킬 사용이 종료된 뒤 동작 Method
    /// </summary>
    public void EndAction();
}