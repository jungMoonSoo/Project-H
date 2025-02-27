using UnityEngine;

public class CheckJustHit : MonoBehaviour, IHitObjectCheckEvent
{
    public bool moveToTarget = true;

    public void Init(HitObjectBase @base)
    {
        if (moveToTarget) transform.position = @base.TargetPos;
    }

    public void Check(HitObjectBase @base)
    {
        @base.OnTrigger();
        @base.OnFinish();
    }
}
