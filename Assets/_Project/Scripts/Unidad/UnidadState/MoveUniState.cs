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
        Unidad[] enemys = GameObject.FindObjectsOfType<Unidad>().Where(x => Unit.Owner != x.Owner).ToArray();
        if (enemys.Length > 0)
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

    public void OnExit()
    {
        
    }
}