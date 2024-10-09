using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isAlly;
    public bool notMove;

    public float moveSpeed;

    public UnitState state;

    private bool onEllipse;
    private Unit onEllipseTarget;
    private Vector3 movePos;

    private float checkDist;
    private float checkClosetDist;

    private Animator animator;
    private EllipseCollider ellipseCollider;

    private Unit target;

    private void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out ellipseCollider);

        ellipseCollider.SetArea(UnitManager.Instance.mapPos, UnitManager.Instance.mapSize);
    }

    private void FixedUpdate()
    {
        SetTarget(isAlly);

        if (target == null) state = UnitState.Idle;

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
    
    private void SetTarget(bool _isAlly)
    {
        if (target == null) checkClosetDist = float.MaxValue;
        else checkClosetDist = ellipseCollider.OnEllipseEnter(transform.position, target.ellipseCollider, EllipseNum.Unit, EllipseNum.Unit);

        if (checkClosetDist <= 1) return;

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != this && UnitManager.Instance.units[i].isAlly == !_isAlly)
            {
                checkDist = ellipseCollider.OnEllipseEnter(transform.position, UnitManager.Instance.units[i].ellipseCollider, EllipseNum.Attack, EllipseNum.Unit);

                if (checkDist < checkClosetDist)
                {
                    checkClosetDist = checkDist;

                    target = UnitManager.Instance.units[i];
                }
            }
        }
    }

    private void Move()
    {
        if (onEllipse)
        {
            if (ellipseCollider.OnEllipseEnter(transform.position, onEllipseTarget.ellipseCollider, EllipseNum.Unit, EllipseNum.Unit) > 1.1f) onEllipse = false;
            else movePos = ellipseCollider.TransAreaPos(movePos + moveSpeed * (transform.position - onEllipseTarget.transform.position).normalized);
        }
        else
        {
            onEllipse = OnEllipseEnter();

            if (movePos == Vector3.zero) state = UnitState.Attack;

            if (movePos.x < 0) transform.localScale = new Vector3(1, 1, 1);
            else if (movePos.x > 0) transform.localScale = new Vector3(-1, 1, 1);
        }

        if (notMove) return;

        animator.Play("Walk");

        transform.position = Vector2.MoveTowards(transform.position, transform.position + movePos, moveSpeed);
    }

    private void Attack()
    {
        if (OnEllipseEnter()) return;

        if (movePos != Vector3.zero)
        {
            state = UnitState.Idle;

            return;
        }

        animator.Play("Attack");

        Debug.Log("Attack the target!");

        if (target.transform.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
        else if (target.transform.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
    }

    private bool OnEllipseEnter()
    {
        if (ellipseCollider.OnEllipseEnter(transform.position, target.ellipseCollider, EllipseNum.Attack, EllipseNum.Unit) <= 1) movePos = Vector3.zero;
        else movePos = ellipseCollider.TransAreaPos(moveSpeed * (target.transform.position - transform.position).normalized);

        for (int i = 0; i < UnitManager.Instance.units.Count; i++)
        {
            if (UnitManager.Instance.units[i] != this && ellipseCollider.OnEllipseEnter(transform.position + movePos, UnitManager.Instance.units[i].ellipseCollider, EllipseNum.Unit, EllipseNum.Unit) <= 1)
            {
                onEllipseTarget = UnitManager.Instance.units[i];
                movePos = ellipseCollider.AroundTarget(movePos, UnitManager.Instance.units[i].ellipseCollider, moveSpeed);

                state = UnitState.Move;

                return true;
            }
        }

        movePos = ellipseCollider.TransAreaPos(movePos);

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
