using UnityEngine;

public class CheckTrackingCaster : MonoBehaviour, IHitObjectCheckEvent
{
    public HitObjectBase HitObject { get; set; }

    public void Check()
    {
        if (HitObject.Caster == null)
        {
            HitObject.OnFinish();

            return;
        }

        transform.position = HitObject.Caster.transform.position;

        HitObject.OnTrigger();
    }
}
