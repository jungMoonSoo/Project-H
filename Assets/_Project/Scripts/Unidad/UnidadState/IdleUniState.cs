using System.Linq;
using UnityEngine;

public class IdleUniState: MonoBehaviour, IUnidadState
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
        Animator.Play("Idle_0");
    }

    public void OnUpdate()
    {
        if (GameObject.FindObjectsOfType<Unidad>().Where(x => Unit.Owner != x.Owner).Count() > 0)
        {
            Unit.StateChange(UnitState.Move);
        }
    }

    public void OnExit()
    {
        
    }
}