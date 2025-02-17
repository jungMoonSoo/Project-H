using UnityEngine;

public class CheckJustHit : MonoBehaviour, IHitObjectCheckEvent
{
    public void Check(HitObjectBase @base)
    {
        transform.position = @base.TargetPos;

        @base.OnTrigger();
        @base.OnFinish();
    }
}
