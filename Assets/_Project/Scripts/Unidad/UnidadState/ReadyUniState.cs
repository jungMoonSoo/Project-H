using UnityEngine;

public class ReadyUniState : MonoBehaviour, IUnidadState
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

        Unit.touchCollider.SetActiveCollider(false);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
