using UnityEngine;

public class CheckTrackingCaster : MonoBehaviour, IHitObjectCheckEvent
{
    public Unidad caster;

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
