using Spine.Unity;
using UnityEngine;

public class SkillUniState: MonoBehaviour, IUnidadState
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
        skeletonAnimation.AnimationState.SetAnimation(0, playAnimation, false).Complete += EndUse;
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        
    }

    private void EndUse(Spine.TrackEntry trackEntry) => Unit.ChangeState(UnitState.Attack);
}