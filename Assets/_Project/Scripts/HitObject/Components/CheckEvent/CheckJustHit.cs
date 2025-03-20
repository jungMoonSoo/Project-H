using UnityEngine;

public class CheckJustHit : MonoBehaviour, IHitObjectCheckEvent
{
    /// <summary>
    /// Target 위치로 이동 후 공격을 진행할지 선택
    /// </summary>
    public bool moveToTarget = true;

    public void Init(Unidad caster)
    {

    }

    public void OnEvent(HitObject hitObject)
    {
        if (moveToTarget) transform.position = hitObject.TargetPos;

        hitObject.OnTrigger();
        hitObject.OnFinish();
    }
}
