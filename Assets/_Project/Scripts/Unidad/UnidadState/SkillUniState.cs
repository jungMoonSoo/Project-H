using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]

    #region ◇ Spine Settings ◇
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    [SerializeField] private SpineEventData[] events;

    private Spine.Animation playAnimation;
    private readonly Dictionary<string, UnityEvent> eventHandles = new();
    #endregion

    [SerializeField] private bool useSpineEvent = false;

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
        skeletonAnimation.AnimationState.SetAnimation(0, playAnimation, false).Complete += EndUse;
        skeletonAnimation.timeScale = Unit.NowNormalStatus.attackSpeed;
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        if (!useSpineEvent) Unit.SkillHandle.Spawn(Unit);
    }

    private void AnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (!useSpineEvent) return;

        if (eventHandles.ContainsKey(e.Data.Name)) eventHandles[e.Data.Name].Invoke();
    }

    private void EndUse(Spine.TrackEntry trackEntry) => Unit.ChangeState(UnitState.Attack);
}