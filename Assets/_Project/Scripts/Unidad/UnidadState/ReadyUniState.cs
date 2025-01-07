using Spine.Unity;
using UnityEngine;

public class ReadyUniState : MonoBehaviour, IUnidadState
{
    [Header("Settings")]
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    private Spine.Animation playAnimation;

    public Unidad Unit
    {
        get;
        set;
    }

    private void Awake()
    {
        playAnimation = skeletonAnimation.skeleton.Data.FindAnimation(animationName);
    }

    public void OnEnter()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, playAnimation, true);

        Unit.touchCollider.SetActiveCollider(false);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
