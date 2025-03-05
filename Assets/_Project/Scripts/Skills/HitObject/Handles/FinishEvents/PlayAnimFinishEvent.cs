using UnityEngine;

public class PlayAnimFinishEvent : MonoBehaviour, IHitObjectFinishEvent
{
    public void OnFinish(HitObject hitObject)
    {
        hitObject.AnimController.PlayAnim();
    }
}
