using UnityEngine;

public class CheckTrackingCaster : MonoBehaviour, IHitObjectCheckEvent
{
    public void Init(HitObject @base)
    {

    }

    public void Check(HitObject @base)
    {
        if (@base.Caster == null)
        {
            @base.OnFinish();

            return;
        }

        transform.position = @base.Caster.transform.position;

        @base.OnTrigger();
    }
}
