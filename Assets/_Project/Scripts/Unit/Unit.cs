using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isAlly;
    public bool notMove;

    public UnitStatus status;
    public UnitStateBase stateBase;

    public LerpSprite hpBar = new();

    public Animator Animator { get; set; }
    public EllipseCollider EllipseCollider { get; set; }

    private UnitState state;
    private Vector3 existingPos;

    private readonly LerpAction lerpAction = new();

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

        status.hp[0].SetBind(HpBind);
    }

    private void FixedUpdate()
    {
        lerpAction.actions?.Invoke();

        stateBase.SetTarget(isAlly);

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
    }

    // 바인딩
    private void HpBind(ref int _current, int _change)
    {
        if (_change < 0) _change = 0;
        else if (_change > status.hp[1].Data) _change = status.hp[1].Data;

        _current = _change;

        hpBar.SetData(lerpAction, (float)_current / status.hp[1].Data);

        // if (_current == 0) Die();
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
