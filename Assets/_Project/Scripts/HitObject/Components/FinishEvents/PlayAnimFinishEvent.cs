using UnityEngine;

public class PlayAnimFinishEvent : MonoBehaviour, IHitObjectFinishEvent
{
    [SerializeField] private AnimatorEventController animController;

    public void OnEvent(HitObject hitObject)
    {
        animController.PlayAnim();
    }
}
