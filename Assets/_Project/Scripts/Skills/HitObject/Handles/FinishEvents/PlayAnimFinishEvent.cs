using UnityEngine;

public class PlayAnimFinishEvent : MonoBehaviour, IHitObjectFinishEvent
{
    [SerializeField] private AnimationEventController animController;

    public void OnEvent(HitObject hitObject)
    {
        animController.PlayAnim();
    }
}
