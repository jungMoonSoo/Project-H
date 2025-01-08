using Spine.Unity;
using UnityEngine;

public class IdleUniState: MonoBehaviour, IUnidadState
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

    public void Init()
    {
        playAnimation = skeletonAnimation.skeleton.Data.FindAnimation(animationName);
    }

    public void OnEnter()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, playAnimation, true);
    }

    public void OnUpdate()
    {
        if (UnidadManager.Instance.GetUnidads(Unit.Owner, TargetType.They).Count > 0)
        {
            Unit.StateChange(UnitState.Move);
        }
    }

    public void OnExit()
    {
        
    }
}