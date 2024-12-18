using UnityEngine;

public interface IActionSkill
{
    /// <summary>
    /// 스킬 드래그 위치 표시용 이펙트 오브젝트
    /// </summary>
    public GameObject AreaEffect
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
    /// <param name="screenPosition">터치가 된 MainCamera 기준 위치</param>
    public void OnDrag(Vector3 screenPosition);
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