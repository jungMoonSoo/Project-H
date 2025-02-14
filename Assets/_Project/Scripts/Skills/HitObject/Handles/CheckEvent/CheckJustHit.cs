using UnityEngine;

public class CheckJustHit : MonoBehaviour, IHitObjectCheckEvent
{
    public HitObjectBase HitObject { get; set; }

    public void Check()
    {
        transform.position = HitObject.TargetPos;

        HitObject.OnTrigger();
        HitObject.OnFinish();
    }
}
