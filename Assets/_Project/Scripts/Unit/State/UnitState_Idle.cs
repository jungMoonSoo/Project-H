using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : UnitStateBase
{
    private Unit onTarget;
    private bool onEllipse;

    private float checkDist;
    private float checkClosestDist;

    private Vector3 MovePos
    {
        get => unit.MovePos;
        set => unit.MovePos = value.normalized;
    }

    public UnitState_Idle(Unit _unit) : base(_unit)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        SetTarget();

        if (unit.Target == null)
        {
            PlayIdleAnim();

            return;
        }

        if (!UnitManager.Instance.isPlay) return;

        if (!HandleUseSkill()) HandleMovement();
    }

    public override void OnExit()
    {

    }

    private void PlayIdleAnim() => unit.Animator.Play($"Idle_{unit.StateNum}");

    private bool HandleUseSkill()
    {
        if (unit.CheckSkill())
        {
            unit.Status.mp[0].Data = 0;
            unit.StateChange(UnitState.Skill);

            return true;
        }

        return false;
    }

    private void HandleMovement()
    {
        if (onEllipse) HandleOnEllipseMove();
        else if (CheckAttackable()) return;

        if (unit.notMove) PlayIdleAnim();
        else
        {
            Flip(unit.Target.transform.position.x, unit.transform.position.x);
            unit.StateChange(UnitState.Move);
        }
    }

    private void HandleOnEllipseMove()
    {
        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, onTarget.EllipseCollider, EllipseType.Unit, EllipseType.Unit) > 1.5f) onEllipse = false;
        else MovePos = unit.EllipseCollider.TransAreaPos(MovePos + CalculateMoveVector(unit.transform, onTarget.transform));
    }

    private bool CheckAttackable()
    {
        onEllipse = CheckEllipseEnter();

        Vector3 _transPos = unit.EllipseCollider.TransAreaPos(MovePos);

        if (MovePos != _transPos)
        {
            MovePos = _transPos;

            return false;
        }

        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, unit.Target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1)
        {
            Flip(unit.Target.transform.position.x, unit.transform.position.x);

            if (unit.notMove) unit.StateChange(UnitState.Attack);
            else if (!onEllipse) unit.StateChange(UnitState.Attack);

            return true;
        }

        Flip(MovePos.x, 0);

        return false;
    }

    private bool CheckEllipseEnter()
    {
        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, unit.Target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1) MovePos = Vector3.zero;
        else MovePos = CalculateMoveVector(unit.Target.transform, unit.transform);

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] == unit) continue;

            if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position + MovePos, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1)
            {
                onTarget = UnitManager.Instance.units[i];
                MovePos = unit.EllipseCollider.AroundTarget(MovePos, onTarget.EllipseCollider, unit.Status.moveSpeed);

                return true;
            }
        }

        return false;
    }

    private void SetTarget()
    {
        unit.Target = null;

        if (!UnitManager.Instance.isPlay) return;

        checkClosestDist = float.MaxValue;

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i].unitType == unit.unitType) continue;

            checkDist = unit.EllipseCollider.OnEllipseEnter(unit.transform.position, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Attack, EllipseType.Unit);

            if (checkDist < checkClosestDist)
            {
                checkClosestDist = checkDist;
                unit.Target = UnitManager.Instance.units[i];
            }
        }

        if (unit.Target == null) UnitManager.Instance.End();
    }

    private Vector3 CalculateMoveVector(Transform _from, Transform _to) => unit.Status.moveSpeed * (_from.position - _to.position).normalized;

    private void Flip(float _targetX, float _currentX)
    {
        unit.transform.rotation = Quaternion.Euler(0, _targetX < _currentX ? 0 : 180, 0);
    }
}
