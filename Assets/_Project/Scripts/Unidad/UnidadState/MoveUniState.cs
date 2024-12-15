using System.Linq;
using UnityEngine;

public class MoveUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
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
        Unidad[] enemys = UnidadManager.Instance.unidades.Where(x => Unit.Owner != x.Owner).OrderBy(unit => Vector3.Distance(unit.transform.position, transform.position)).ToArray();

        if (enemys.Length > 0)
        {
            Vector2 _movePos = MapManager.Instance.ClampPositionToMap(Unit.transform.position, Unit.unitCollider.size);

            if ((Vector2)Unit.transform.position != _movePos)
            {
                Unit.transform.position = Vector2.MoveTowards(Unit.transform.position, _movePos, moveSpeed * Time.deltaTime);
            }
            else
            {
                Unidad target = enemys[0];

                if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider))
                {
                    Vector2 direction = target.unitCollider.transform.position - transform.position;

                    Unit.transform.eulerAngles = new Vector2(0, direction.x > 0 ? 180 : 0);
                    Unit.transform.position = Vector2.MoveTowards(Unit.transform.position, target.transform.position, moveSpeed * Time.deltaTime);
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