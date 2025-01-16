using Spine.Unity;
using UnityEngine;

public class DieUniState: MonoBehaviour, IUnidadState
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
        Unit.DieEvent?.Invoke();
        Destroy(Unit.gameObject);
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}