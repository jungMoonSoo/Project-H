using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]

    #region ◇ Spine Settings ◇
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField, SpineAnimation(dataField: "skeletonAnimation")] private string animationName;

    [SerializeField] private SpineEventData[] events;

    private Spine.Animation playAnimation;
    private readonly Dictionary<string, UnityEvent> eventHandles = new();
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
        skeletonAnimation.timeScale = Unit.NowNormalStatus.moveSpeed * 0.5f;
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

    private void AnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (eventHandles.ContainsKey(e.Data.Name)) eventHandles[e.Data.Name].Invoke();
    }
}