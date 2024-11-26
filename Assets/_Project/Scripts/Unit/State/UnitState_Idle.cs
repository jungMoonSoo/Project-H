using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState_Idle : UnitStateBase
{
    private float checkDist;
    private float checkClosestDist;

    public UnitState_Idle(Unit _unit, UnitStateBase _base) : base(_unit, _base)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        SetTarget();

        if (target == null)
        {
            PlayIdleAnimation();

            return;
        }

        if (!UnitManager.Instance.isPlay) return;

        HandleSkillActivation();
        HandleMovement();
    }

    public override void OnExit()
    {

    }

    private void PlayIdleAnimation() => unit.Animator.Play($"Idle_{unit.StateNum}");

    private void HandleSkillActivation()
    {
        if (unit.CheckSkill())
        {
            unit.Status.mp[0].Data = 0;
            unit.StateChange(UnitState.Skill);
        }
    }

    private void HandleMovement()
    {
        if (onEllipse) HandleEllipseMovement();
        else if (CheckEllipseEnter()) return;

        if (unit.notMove) PlayIdleAnimation();
        else
        {
            Flip(target.transform.position.x, unit.transform.position.x);
            unit.StateChange(UnitState.Move);
        }
    }

    private void HandleEllipseMovement()
    {
        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, onTarget.EllipseCollider, EllipseType.Unit, EllipseType.Unit) > 1.5f) onEllipse = false;
        else movePos = unit.EllipseCollider.TransAreaPos(movePos + CalculateMoveVector(unit.transform, onTarget.transform));
    }

    private bool CheckEllipseEnter()
    {
        onEllipse = false;

        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1) movePos = Vector3.zero;
        else movePos = CalculateMoveVector(target.transform, unit.transform);

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] == unit) continue;

            if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position + movePos, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Unit, EllipseType.Unit) <= 1)
            {
                onTarget = UnitManager.Instance.units[i];
                movePos = unit.EllipseCollider.AroundTarget(movePos, onTarget.EllipseCollider, unit.Status.moveSpeed);
                onEllipse = true;

                break;
            }
        }

        if (!onEllipse) movePos = unit.EllipseCollider.TransAreaPos(movePos);

        if (unit.EllipseCollider.OnEllipseEnter(unit.transform.position, target.EllipseCollider, EllipseType.Attack, EllipseType.Unit) <= 1)
        {
            Flip(target.transform.position.x, unit.transform.position.x);
            TransitionToAttackState();

            return true;
        }

        Flip(movePos.x, 0);

        return false;
    }

    private void SetTarget()
    {
        target = null;

        if (!UnitManager.Instance.isPlay) return;

        checkClosestDist = float.MaxValue;

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i].unitType == unit.unitType) continue;

            checkDist = unit.EllipseCollider.OnEllipseEnter(unit.transform.position, UnitManager.Instance.units[i].EllipseCollider, EllipseType.Attack, EllipseType.Unit);

            if (checkDist < checkClosestDist)
            {
                checkClosestDist = checkDist;
                target = UnitManager.Instance.units[i];
            }
        }

        if (target == null) UnitManager.Instance.End();
    }

    private void TransitionToAttackState()
    {
        if (unit.notMove || !onEllipse) unit.StateChange(UnitState.Attack);
    }

    private Vector3 CalculateMoveVector(Transform _from, Transform _to) => unit.Status.moveSpeed * (_from.position - _to.position).normalized;

    private void Flip(float _targetX, float _currentX)
    {
        unit.transform.rotation = Quaternion.Euler(0, _targetX < _currentX ? 0 : 180, 0);
    }
}
