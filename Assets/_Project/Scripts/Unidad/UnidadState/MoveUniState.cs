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
        
    }

    public void OnExit()
    {
        
    }
}