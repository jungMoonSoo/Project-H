using Spine.Unity;
using UnityEngine;

public class AttackUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]

    #region ◇ Spine Settings ◇
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    [SerializeField, SpineEvent(dataField: "skeletonAnimation")] private string hitPoint;
    [SerializeField, SpineEvent(dataField: "skeletonAnimation")] private string soundPoint;

    private Spine.Animation playAnimation;

    private Spine.EventData hitEvent;
    private Spine.EventData soundEvent;
    #endregion

    [SerializeField] private int audioClipNumber = -1;
    [SerializeField] private float attackPoint = 0.5f;
    [SerializeField] private float attackSoundPoint = 0.5f;

    private bool attack;
    private bool sound;

    private Unidad target;

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

        hitEvent = skeletonAnimation.Skeleton.Data.FindEvent(hitPoint);
        soundEvent = skeletonAnimation.Skeleton.Data.FindEvent(soundPoint);

        skeletonAnimation.AnimationState.Event += AnimationEvent;
    }

    public void OnEnter()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, playAnimation, true);

        unidadTargeting = TargetingTypeHub.GetTargetingSystem(targetingType);
    }

    public void OnUpdate()
    {
        Vector2 movePos = MapManager.Instance.ClampPositionToMap(Unit.transform.position, Unit.unitCollider.Radius);

        if ((Vector2)Unit.transform.position != movePos)
        {
            Unit.StateChange(UnitState.Move);

            return;
        }

        if (unidadTargeting.TryGetTargets(out Unidad[] enemys, Unit.Owner, Unit.attackCollider, 1))
        {
            target = enemys[0];
            Vector2 direction = target.unitCollider.transform.position - transform.position;

            Unit.transform.eulerAngles = new Vector2(0, direction.x < 0 ? 180 : 0);

            if (skeletonAnimation.AnimationState.GetCurrent(0).AnimationTime > attackPoint)
            {
                if (!attack)
                {
                    unidadTargeting.OnEvent(Unit, target);

                    attack = true;
                }
            }
            else attack = false;

            if (skeletonAnimation.AnimationState.GetCurrent(0).AnimationTime > attackSoundPoint)
            {
                if (!sound)
                {
                    PlaySound();

                    sound = true;
                }
            }
            else sound = false;

            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider)) Unit.StateChange(UnitState.Move);
        }
        else Unit.StateChange(UnitState.Move);
    }

    public void OnExit()
    {
        
    }

    private void AnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data == hitEvent) unidadTargeting.OnEvent(Unit, target);
        else if (e.Data == soundEvent) PlaySound();
    }

    private void PlaySound()
    {
        if (audioClipNumber != -1) Unit.audioHandle.OnPlay(audioClipNumber);
    }
}