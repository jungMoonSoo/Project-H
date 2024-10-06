using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yang
{
    public class PathFinding : MonoBehaviour
    {
        public float attackRange;
        public float unitSize;
        public float heightRatio;

        private Unit unit;

        private void Start()
        {
            TryGetComponent(out unit);
        }

        public Vector3 AroundTarget(PathFinding _target)
        {
            Vector3 _movePos = unit.moveSpeed * Time.deltaTime * (transform.position - _target.transform.position).normalized;

            Vector3 _rightTurn = Quaternion.Euler(0, 0, -90) * _movePos;
            Vector3 _leftTurn = Quaternion.Euler(0, 0, 90) * _movePos;

            if (OnHitEllipse(_rightTurn, _target) > 1) _movePos += _rightTurn;
            else if (OnHitEllipse(_leftTurn, _target) > 1) _movePos += _leftTurn;

            return _movePos;
        }

        public float OnHitEllipse(Vector3 _pos, PathFinding _target)
        {
            Vector2 delta = _target.transform.position - _pos;

            return Mathf.Pow(delta.x / (unitSize + _target.unitSize), 2) + Mathf.Pow(delta.y / (unitSize * heightRatio + _target.unitSize * _target.heightRatio), 2);
        }

        public float OnAttackEllipse(Vector3 _pos, PathFinding _target)
        {
            Vector2 delta = _target.transform.position - _pos;

            return Mathf.Pow(delta.x / (attackRange + _target.unitSize), 2) + Mathf.Pow(delta.y / (attackRange * heightRatio + _target.unitSize * _target.heightRatio), 2);
        }
    }
}
