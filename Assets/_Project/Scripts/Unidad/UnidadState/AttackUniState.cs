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
    [SerializeField] private bool useSpineEvent = false;
    [SerializeField] private int audioClipNumber = -1;
    [SerializeField] private float attackPoint = 0.5f;
    [SerializeField] private float attackSoundPoint = 0.5f;

    [SerializeField] private TrackingManager trackingManager;
    [SerializeField] private GameObject attackEffect;

    private bool attack;
    private bool sound;

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

        skeletonAnimation.AnimationState.Event -= AnimationEvent;
        skeletonAnimation.AnimationState.Event += AnimationEvent;
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
            Unit.ChangeState(UnitState.Move);

            return;
        }

        UnitState state = trackingManager.CheckState(Unit);

        switch (state)
        {
            case UnitState.Attack:
                if (useSpineEvent) break;

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
                break;

            default:
                Unit.ChangeState(state);
                break;
        }
    }

    public void OnExit()
    {

    }

    private void AnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (eventHandles.ContainsKey(e.Data.Name)) eventHandles[e.Data.Name].Invoke();
    }

    public void OnAttack()
    {
        // if (attackEffect != null) attackEffect.SetActive(true);

        trackingManager.CreateHitObject(Unit);
    }

    public void PlaySound()
    {
        if (audioClipNumber != -1) Unit.audioHandle.OnPlay(audioClipNumber);
    }
}