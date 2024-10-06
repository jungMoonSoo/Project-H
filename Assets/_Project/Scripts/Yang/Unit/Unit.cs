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

        private bool hit;
        private Vector3 movePos;

        private float checkDist;
        private float checkClosetDist;

        private Animator animator;
        private PathFinding pathFinding;

        private UnitManager manager;

        public void Init(UnitManager _manager)
        {
            manager = _manager;

            TryGetComponent(out animator);
            TryGetComponent(out pathFinding);
        }

        private void Update()
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

            hit = false;
            movePos = Vector3.zero;

            if (pathFinding.OnAttackEllipse(transform.position, target.pathFinding) <= 1)
            {
                if (pathFinding.OnHitEllipse(transform.position, target.pathFinding) <= 1)
                {
                    movePos = pathFinding.AroundTarget(target.pathFinding);

                    state = UnitState.Move;
                    hit = true;
                }
                else state = UnitState.Attack;
            }
            else
            {
                for (int i = 0; i < manager.units.Count; i++)
                {
                    if (manager.units[i] != this && pathFinding.OnHitEllipse(transform.position, manager.units[i].pathFinding) <= 1)
                    {
                        movePos += pathFinding.AroundTarget(manager.units[i].pathFinding);

                        state = UnitState.Move;
                        hit = true;
                    }
                }

                if (movePos == Vector3.zero) movePos = moveSpeed * Time.deltaTime * (target.transform.position - transform.position).normalized;
            }

            transform.position += movePos;

            if (!hit)
            {
                if (movePos.x < 0) transform.localScale = new Vector3(1, 1, 1);
                else if (movePos.x > 0) transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private void Attack()
        {
            if (target == null)
            {
                state = UnitState.Idle;

                return;
            }

            animator.Play("Attack");

            if (pathFinding.OnHitEllipse(transform.position, target.pathFinding) <= 1)
            {
                state = UnitState.Move;

                return;
            }

            if (pathFinding.OnAttackEllipse(transform.position, target.pathFinding) <= 1) Debug.Log("Attack the target!");
            else state = UnitState.Move;

            if (target.transform.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
            else if (target.transform.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public enum UnitState
    {
        Idle,
        Move,
        Attack,
        Die
    }
}
