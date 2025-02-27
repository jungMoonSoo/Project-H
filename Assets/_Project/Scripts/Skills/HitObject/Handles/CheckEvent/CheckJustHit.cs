using UnityEngine;

public class CheckJustHit : MonoBehaviour, IHitObjectCheckEvent
{
    public bool moveToTarget = true;

    public void Init(HitObject @base)
    {
        if (moveToTarget) transform.position = @base.TargetPos;
    }

    public void Check(HitObject @base)
    {
        @base.OnTrigger();
        @base.OnFinish();
    }
}
