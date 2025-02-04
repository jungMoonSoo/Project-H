using Spine.Unity;
using UnityEngine;

public class MoveUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    private Spine.Animation playAnimation;

    private UnidadTargetingType targetingType = UnidadTargetingType.Near;
    private IUnidadTargeting unidadTargeting;

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

        unidadTargeting = TargetingTypeHub.GetTargetingSystem(targetingType);
    }

    public void OnUpdate()
    {
        Vector3 movePos = MapManager.Instance.ClampPositionToMap(Unit.transform.position, Unit.unitCollider.Radius);

        if (Unit.transform.position != movePos)
        {
            Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, movePos, Unit.NowNormalStatus.moveSpeed * Time.deltaTime);

            return;
        }

        if (unidadTargeting.TryGetTargets(out Unidad[] enemys, Unit.Owner, Unit.attackCollider, 1))
        {
            Unidad target = enemys[0];

            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider))
            {
                Vector3 direction = target.unitCollider.transform.position - transform.position;

                Unit.transform.eulerAngles = new Vector2(0, direction.x < 0 ? 180 : 0);
                Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, target.transform.position, Unit.NowNormalStatus.moveSpeed * Time.deltaTime);
            }
            else Unit.ChangeState(UnitState.Attack);
        }
        else Unit.ChangeState(UnitState.Idle);
    }

    public void OnExit()
    {
        
    }
}