using System.Collections.Generic;
using UnityEngine;

namespace Yang
{
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
        private EllipseCollider pathFinding;

        private UnitManager manager;

        public void Init(UnitManager _manager)
        {
            manager = _manager;

            TryGetComponent(out animator);
            TryGetComponent(out pathFinding);
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
            else checkClosetDist = pathFinding.OnHitEllipse(transform.position, target.pathFinding);

            if (checkClosetDist <= 1) return;

            for (int i = 0; i < manager.units.Count; i++)
            {
                if (manager.units[i] != this && manager.units[i].enemy == !_enemy)
                {
                    checkDist = pathFinding.OnAttackEllipse(transform.position, manager.units[i].pathFinding);

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
                if (pathFinding.OnHitEllipse(transform.position, onEllipseTarget.pathFinding) > 1.1f) onEllipse = false;
            }
            else
            {
                onEllipse = OnEllipseEnter();

                if (pathFinding.OnAttackEllipse(transform.position, target.pathFinding) <= 1) state = UnitState.Attack;

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

            if (OnEllipseEnter())
            {
                state = UnitState.Move;

                return;
            }

            if (pathFinding.OnAttackEllipse(transform.position, target.pathFinding) > 1)
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
                if (manager.units[i] != this && pathFinding.OnHitEllipse(transform.position + movePos, manager.units[i].pathFinding) <= 1)
                {
                    onEllipseTarget = manager.units[i];
                    movePos = pathFinding.AroundTarget(manager.units[i].pathFinding, movePos);

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
}
