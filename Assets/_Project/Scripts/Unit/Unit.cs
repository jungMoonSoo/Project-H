using System;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isAlly;
    public bool notMove;

    public Action skills;

    public UnitStatus status;
    public UnitStateBase stateBase;

    public LerpSprite hpBar = new();

    public Animator Animator { get; set; }
    public EllipseCollider EllipseCollider { get; set; }

    private UnitState state;
    private Vector3 existingPos;

    private readonly UnitBind unitBind = new();

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying) UnitManager.Instance.units.Add(this);
#else
        UnitManager.Instance.units.Add(this);
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying) UnitManager.Instance.units.Remove(this);
#else
        UnitManager.Instance.units.Remove(this);
#endif
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        EllipseCollider = GetComponent<EllipseCollider>();

        EllipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);

        state = UnitState.Idle;
        stateBase = stateBase = new UnitState_Idle(this);

        unitBind.Init(this);
    }

    private void FixedUpdate()
    {
        unitBind.Action();

        stateBase.SetTarget();
        stateBase.OnUpdate();
    }

    public void StateChange(UnitState _state)
    {
        if (state == _state) return;

        stateBase.OnExit();

        state = _state;

        switch (state)
        {
            case UnitState.Idle:
                stateBase = new UnitState_Idle(this);
                break;

            case UnitState.Move:
                stateBase = new UnitState_Move(this);
                break;

            case UnitState.Attack:
                stateBase = new UnitState_Attack(this);
                break;

            case UnitState.Die:
                stateBase = new UnitState_Die(this);
                break;
        }

        stateBase.OnEnter();
    }

    public void SetPos(Vector2 _pos)
    {
        existingPos = _pos;

        ReturnPos();
    }

    public void ReturnPos()
    {
        transform.position = existingPos;

        if (isAlly) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (stateBase == null) return;

        Gizmos.color = Color.gray;

        Gizmos.DrawLine(transform.position, transform.position + stateBase.GetMovePos() * 5);
    }
#endif
}
