using System.Collections.Generic;
using UnityEngine;

namespace Yang
{
    public class Unit : MonoBehaviour
    {
        public float attackRange;
        public float unitSize;
        public float heightRatio;

        public bool enemy;
        public float moveSpeed;

        public UnitState state;

        public Unit target;

        private bool hit;
        private Vector3 movePos;

        private float checkDist;
        private float checkClosetDist;

        private UnitManager manager;
        private Animator animator;

        public void Init(UnitManager _manager)
        {
            manager = _manager;
            TryGetComponent(out animator);
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
            else checkClosetDist = OnEllipse(transform.position, target, attackRange, target.unitSize);

            if (checkClosetDist <= 1) return;

            for (int i = 0; i < manager.units.Count; i++)
            {
                if (manager.units[i] != this && manager.units[i].enemy == !_enemy)
                {
                    checkDist = OnEllipse(transform.position, manager.units[i], attackRange, manager.units[i].unitSize);

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

            if (OnEllipse(transform.position, target, attackRange, target.unitSize) <= 1)
            {
                if (OnEllipse(transform.position, target, unitSize, target.unitSize) <= 1) movePos = AroundTarget(target);
                else state = UnitState.Attack;
            }
            else
            {
                for (int i = 0; i < manager.units.Count; i++)
                {
                    if (manager.units[i] != this && OnEllipse(transform.position, manager.units[i], unitSize, manager.units[i].unitSize) <= 1) movePos += AroundTarget(manager.units[i]);
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

            if (OnEllipse(transform.position, target, unitSize, target.unitSize) <= 1)
            {
                state = UnitState.Move;

                return;
            }

            if (OnEllipse(transform.position, target, attackRange, target.unitSize) <= 1) Debug.Log("Attack the target!");
            else state = UnitState.Move;

            if (target.transform.position.x < transform.position.x) transform.localScale = new Vector3(1, 1, 1);
            else if (target.transform.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        }

        private Vector3 AroundTarget(Unit _target)
        {
            Vector3 _movePos = moveSpeed * Time.deltaTime * (transform.position - _target.transform.position).normalized;
        
            Vector3 _rightTurn = Quaternion.Euler(0, 0, -90) * _movePos;
            Vector3 _leftTurn = Quaternion.Euler(0, 0, 90) * _movePos;

            if (OnEllipse(_rightTurn, _target, unitSize, _target.unitSize) > 1) _movePos += _rightTurn;
            else if (OnEllipse(_leftTurn, _target, unitSize, _target.unitSize) > 1) _movePos += _leftTurn;
        
            state = UnitState.Move;
            hit = true;
        
            return _movePos;
        }

        private float OnEllipse(Vector3 _pos, Unit _target, float _range, float _targetRange)
        {
            Vector2 delta = _target.transform.position - _pos;

            return Mathf.Pow(delta.x / (_range + _targetRange), 2) + Mathf.Pow(delta.y / (_range * heightRatio + _targetRange * _target.heightRatio), 2);
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
