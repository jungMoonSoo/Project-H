using UnityEngine;

public class SkillUniState: MonoBehaviour, IUnidadState
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
        Animator.Play("Skill _0");
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}