using UnityEngine;

public class PlayAnimFinishEvent : MonoBehaviour, IHitObjectFinishEvent
{
    [SerializeField] private AnimationEventController animController;

    public void OnFinish(HitObject hitObject)
    {
        animController.PlayAnim();
    }
}
