using UnityEngine;

public class CheckTrackingCaster : MonoBehaviour, IHitObjectCheckEvent
{
    /// <summary>
    /// 시전자 추적용, 사망 전까지 추적 후 사망 시 종료
    /// </summary>
    private Unidad caster;

    public void Init(Unidad caster)
    {
        this.caster = caster;
    }

    public void OnEvent(HitObject hitObject)
    {
        if (caster == null)
        {
            hitObject.OnFinish();

            return;
        }

        transform.position = caster.transform.position;

        hitObject.OnTrigger();
    }
}
