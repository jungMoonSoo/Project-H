using UnityEngine;

public class CheckJustHit : MonoBehaviour, IHitObjectCheckEvent
{
    public void Init(HitObjectBase @base)
    {
        transform.position = @base.TargetPos;
    }

    public void Check(HitObjectBase @base)
    {
        @base.OnTrigger();
        @base.OnFinish();
    }
}
