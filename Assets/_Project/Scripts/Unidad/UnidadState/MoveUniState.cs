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

    [SerializeField] private GameObject attackEventHandle;

    private IUnidadAttack unidadAttack;

    public Unidad Unit
    {
        get;
        set;
    }

    public void Init()
    {
        playAnimation = skeletonAnimation.skeleton.Data.FindAnimation(animationName);

        attackEventHandle.TryGetComponent(out unidadAttack);
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

        UnitState state = unidadAttack.Check(Unit);

        switch (state)
        {
            case UnitState.Move:
                Unit.transform.position = Vector3.MoveTowards(Unit.transform.position, unidadAttack.Targets[0].transform.position, Unit.NowNormalStatus.moveSpeed * Time.deltaTime);
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