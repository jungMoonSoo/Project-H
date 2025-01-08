using Spine.Unity;
using UnityEngine;

public class AttackUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    [SerializeField, SpineEvent(dataField: "skeletonAnimation")] private string hitPoint;
    [SerializeField, SpineEvent(dataField: "skeletonAnimation")] private string soundPoint;

    [SerializeField] private int audioClipNumber = -1;

    private Spine.Animation playAnimation;

    private Spine.EventData hitEvent;
    private Spine.EventData soundEvent;

    private Unidad target;
    private UnidadTargetingType targetingType = UnidadTargetingType.Near;

    public Unidad Unit
    {
        get;
        set;
    }

    public void Init()
    {
        playAnimation = skeletonAnimation.skeleton.Data.FindAnimation(animationName);

        hitEvent = skeletonAnimation.Skeleton.Data.FindEvent(hitPoint);
        soundEvent = skeletonAnimation.Skeleton.Data.FindEvent(soundPoint);

        skeletonAnimation.AnimationState.Event += AnimationEvent;
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
            Unit.StateChange(UnitState.Move);

            return;
        }

        Unidad[] enemys = TargetingTypeHub.GetTargetingSystem(targetingType).GetTargets(Unit.Owner, Unit.attackCollider, 1);

        if (enemys.Length > 0)
        {
            target = enemys[0];
            Vector2 direction = target.unitCollider.transform.position - transform.position;

            Unit.transform.eulerAngles = new Vector2(0, direction.x > 0 ? 180 : 0);

            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider)) Unit.StateChange(UnitState.Move);
        }
        else Unit.StateChange(UnitState.Move);
    }

    public void OnExit()
    {
        
    }

    private void AnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data == hitEvent) CheckAttackPoint();
        else if (e.Data == soundEvent) PlaySound();
    }

    private void CheckAttackPoint()
    {
        CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(Unit.NowAttackStatus, target.NowDefenceStatus, 100, 0, ElementType.None);
        target.OnDamage((int)callback.value, callback.type);

        Unit.IncreaseMp(callback.type == DamageType.Miss ? 0.5f : 1);
    }

    private void PlaySound()
    {
        if (audioClipNumber != -1) Unit.audioHandle.OnPlay(audioClipNumber);
    }
}