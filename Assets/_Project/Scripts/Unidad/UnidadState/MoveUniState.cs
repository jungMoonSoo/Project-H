using System.Linq;
using UnityEngine;

public class MoveUniState: MonoBehaviour, IUnidadState
{
    public Unidad Unit
    {
        get;
        set;
    }
    public Animator Animator
    {
        get;
        set;
    }

    public void OnEnter()
    {
        Animator.Play("Walk_0");
    }

    public void OnUpdate()
    {
        Unidad[] enemys = UnidadManager.Instance.GetUnidads(Unit.Owner, true).OrderBy(unit => Vector2.Distance((Vector2)unit.transform.position + unit.unitCollider.center, transform.position)).ToArray();

        if (enemys.Length > 0)
        {
            Vector2 _movePos = MapManager.Instance.ClampPositionToMap(Unit.transform.position, Unit.unitCollider.size);

            if ((Vector2)Unit.transform.position != _movePos)
            {
                Unit.transform.position = Vector2.MoveTowards(Unit.transform.position, _movePos, Unit.MoveSpeed * Time.deltaTime);
            }
            else
            {
                Unidad target = enemys[0];

                if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider))
                {
                    Vector2 direction = target.unitCollider.transform.position - transform.position;

                    Unit.transform.eulerAngles = new Vector2(0, direction.x > 0 ? 180 : 0);
                    Unit.transform.position = Vector2.MoveTowards(Unit.transform.position, target.transform.position, Unit.MoveSpeed * Time.deltaTime);
                }
                else
                {
                    Unit.StateChange(UnitState.Attack);
                }
            }
        }
        else
        {
            Unit.StateChange(UnitState.Idle);
        }
    }

    public void OnExit()
    {
        
    }
}