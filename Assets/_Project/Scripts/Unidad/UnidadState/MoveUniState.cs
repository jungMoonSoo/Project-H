using Spine.Unity;
using System.Linq;
using UnityEngine;

public class MoveUniState: MonoBehaviour, IUnidadState
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
    }

    public void OnUpdate()
    {
        Vector2 movePos = MapManager.Instance.ClampPositionToMap(Unit.transform.position, Unit.unitCollider.Radius);

        if ((Vector2)Unit.transform.position != movePos)
        {
            Unit.transform.position = Vector2.MoveTowards(Unit.transform.position, movePos, Unit.MoveSpeed * Time.deltaTime);

            return;
        }

        Unidad[] enemys = UnidadManager.Instance.GetUnidads(Unit.Owner, TargetType.They).OrderBy(unit => Vector2.Distance((Vector2)unit.transform.position + unit.unitCollider.center, transform.position)).ToArray();

        if (enemys.Length > 0)
        {
            Unidad target = enemys[0];

            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider))
            {
                Vector2 direction = target.unitCollider.transform.position - transform.position;

                Unit.transform.eulerAngles = new Vector2(0, direction.x > 0 ? 180 : 0);
                Unit.transform.position = Vector2.MoveTowards(Unit.transform.position, target.transform.position, Unit.MoveSpeed * Time.deltaTime);
            }
            else Unit.StateChange(UnitState.Attack);
        }
        else Unit.StateChange(UnitState.Idle);
    }

    public void OnExit()
    {
        
    }
}