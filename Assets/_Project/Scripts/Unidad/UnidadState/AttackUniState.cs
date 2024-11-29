using UnityEngine;

public class AttackUniState: MonoBehaviour, IUnidadState
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
        Animator.Play("Attack_0");
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}