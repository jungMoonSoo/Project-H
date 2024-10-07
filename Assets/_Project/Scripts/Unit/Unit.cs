using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool enemy;
    public float moveSpeed;

    public UnitState state;

    public Unit target;

    private bool onEllipse;
    private Unit onEllipseTarget;
    private Vector3 movePos;

    private float checkDist;
    private float checkClosetDist;

    private Animator animator;
    private EllipseCollider ellipseCollider;

    private UnitManager manager;

    public void Init(UnitManager _manager)
    {
        manager = _manager;

        TryGetComponent(out animator);
        TryGetComponent(out ellipseCollider);
    }

    private void FixedUpdate()
    {
        if (manager == null) return;

        SetTarget(enemy);

        switch (state)
        {
            case UnitState.Idle:
                animator.Play("Idle");

                if (target != null) state = UnitState.Move;
                break;

            case UnitState.Move:
                Move();
                break;

            case UnitState.Attack:
                Attack();
                break;

            case UnitState.Die:
                break;
        }
    }

    private void SetTarget(bool _enemy)
    {
        if (target == null) checkClosetDist = float.MaxValue;
        else checkClosetDist = ellipseCollider.OnHitEllipse(transform.position, target.ellipseCollider);

        if (checkClosetDist <= 1) return;

        for (int i = 0; i < manager.units.Count; i++)
        {
            if (manager.units[i] != this && manager.units[i].enemy == !_enemy)
            {
                checkDist = ellipseCollider.OnAttackEllipse(transform.position, manager.units[i].ellipseCollider);

                if (checkDist < checkClosetDist)
                {
                    checkClosetDist = checkDist;
                    target = manager.units[i];
                }
            }
        }
    }

    private void Move()
    {
        if (target == null)
        {
            state = UnitState.Idle;

            return;
        }

        animator.Play("Walk");

        if (onEllipse)
        {
            if (ellipseCollider.OnHitEllipse(transform.position, onEllipseTarget.ellipseCollider) > 1.1f) onEllipse = false;
            else movePos = moveSpeed * (transform.position - onEllipseTarget.transform.position).normalized;
        }
        else
        {
            onEllipse = OnEllipseEnter();

            if (ellipseCollider.OnAttackEllipse(transform.position, target.ellipseCollider) <= 1) state = UnitState.Attack;

            if (movePos.x < 0) transform.localScale = new Vector3(1, 1, 1);
            else if (movePos.x > 0) transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, transform.position + movePos, moveSpeed);
    }

    private void Attack()
    {
        if (target == null)
        {
            state = UnitState.Idle;

            return;
        }

        animator.Play("Attack");

        if (OnEllipseEnter()) return;

        if (ellipseCollider.OnAttackEllipse(transform.position, target.ellipseCollider) > 1)
        {
            state = UnitState.Move;

            return;
        }

        Debug.Log("Attack the target!");

        if (target.transform.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else if (target.transform.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
    }

    private bool OnEllipseEnter()
    {
        movePos = moveSpeed * (target.transform.position - transform.position).normalized;

        for (int i = 0; i < manager.units.Count; i++)
        {
            if (manager.units[i] != this && ellipseCollider.OnHitEllipse(transform.position + movePos, manager.units[i].ellipseCollider) <= 1)
            {
                onEllipseTarget = manager.units[i];
                movePos = ellipseCollider.AroundTarget(manager.units[i].ellipseCollider, movePos);

                state = UnitState.Move;

                return true;
            }
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        Gizmos.DrawLine(transform.position, transform.position + movePos.normalized * 5);
    }
#endif
}

public enum UnitState
{
    Idle,
    Move,
    Attack,
    Die
}
