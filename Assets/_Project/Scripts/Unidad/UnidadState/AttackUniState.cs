using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]

    #region ◇ Spine Settings ◇
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    [SerializeField] private SpineEventData[] events;

    private Spine.Animation playAnimation;
    private readonly Dictionary<string, UnityEvent> eventHandles = new();
    #endregion

    [Header("임시 설정")]
    [SerializeField] private int audioClipNumber = -1;
    [SerializeField] private float attackPoint = 0.5f;
    [SerializeField] private float attackSoundPoint = 0.5f;

    [SerializeField] private GameObject attackEventHandle;
    [SerializeField] private GameObject attackEffect;

    private bool attack;
    private bool sound;

    private Unidad[] targets;

    private UnidadTargetingType targetingType = UnidadTargetingType.Near;

    private IUnidadTargeting unidadTargeting;
    private IUnidadAttack unidadAttack;

    public Unidad Unit
    {
        get;
        set;
    }

    public void Init()
    {
        playAnimation = skeletonAnimation.skeleton.Data.FindAnimation(animationName);

        for (int i = 0; i < events.Length; i++)
        {
            if (events[i].eventName == "") continue;

            eventHandles.Add(events[i].eventName, events[i].unityEvent);
        }

        skeletonAnimation.AnimationState.Event += AnimationEvent;

        attackEventHandle.TryGetComponent(out unidadAttack);
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
            Unit.ChangeState(UnitState.Move);

            return;
        }

        if (unidadTargeting.TryGetTargets(out targets, Unit.Owner, Unit.attackCollider, 1))
        {
            Unidad target = targets[0];

            Vector2 direction = target.unitCollider.transform.position - transform.position;

            Unit.transform.eulerAngles = new Vector2(0, direction.x < 0 ? 180 : 0);

            if (skeletonAnimation.AnimationState.GetCurrent(0).AnimationTime > attackPoint)
            {
                if (!attack)
                {
                    OnAttack();

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

            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider)) Unit.ChangeState(UnitState.Move);
        }
        else Unit.ChangeState(UnitState.Move);
    }

    public void OnExit()
    {
        
    }

    private void AnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e) => eventHandles[e.Data.Name]?.Invoke();

    public void OnAttack()
    {
        // if (attackEffect != null) attackEffect.SetActive(true);

        unidadAttack.OnAttack(Unit, targets);
    }

    public void PlaySound()
    {
        if (audioClipNumber != -1) Unit.audioHandle.OnPlay(audioClipNumber);
    }
}