using Spine.Unity;
using UnityEngine;

public class MoveUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]

    #region ◇ Spine Settings ◇
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    private Spine.Animation playAnimation;
    #endregion

    [SerializeField] private TrackingManager trackingManager;

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
        Vector3 movePos = MapManager.Instance.ClampPositionToMap(Unit.transform.position, Unit.unitCollider.Radius);

        if (Unit.transform.position != movePos)
        {
            Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, movePos, Unit.NowNormalStatus.moveSpeed * Time.deltaTime);

            return;
        }

        UnitState state = trackingManager.CheckState(Unit);

        switch (state)
        {
            case UnitState.Move:
                Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, trackingManager.Targets[0].transform.position, Unit.NowNormalStatus.moveSpeed * Time.deltaTime);

                // Vector3 pos = Teeeest.GetEvasiveVector(Unit, trackingManager.Targets[0]);
                // 
                // Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, pos, Unit.NowNormalStatus.moveSpeed * Time.deltaTime);
                break;

            default:
                Unit.ChangeState(state);
                break;
        }
    }

    public void OnExit()
    {

    }
}