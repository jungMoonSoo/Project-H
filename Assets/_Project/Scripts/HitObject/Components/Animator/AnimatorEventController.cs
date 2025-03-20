using UnityEngine;

public class AnimatorEventController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void StopAnim() => animator.Play("Idle");

    public void PlayAnim() => animator.Play("Play");
}
