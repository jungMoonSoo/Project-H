using UnityEngine;

public class CheckJustHit : MonoBehaviour, IHitObjectCheckEvent
{
    public bool moveToTarget = true;

    public void Init(Unidad caster)
    {

    }

    public void OnEvent(HitObject hitObject)
    {
        if (moveToTarget) transform.position = hitObject.TargetPos;

        hitObject.OnTrigger();
        hitObject.OnFinish();
    }
}
