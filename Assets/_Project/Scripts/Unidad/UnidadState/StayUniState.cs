using UnityEngine;

public class StayUniState : MonoBehaviour, IUnidadState
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

        Unit.touchCollider.SetActiveCollider(true);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
