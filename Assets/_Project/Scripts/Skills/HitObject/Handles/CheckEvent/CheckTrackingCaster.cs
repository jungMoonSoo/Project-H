using UnityEngine;

public class CheckTrackingCaster : MonoBehaviour, IHitObjectCheckEvent
{
    public void Init(HitObjectBase @base)
    {

    }

    public void Check(HitObjectBase @base)
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
