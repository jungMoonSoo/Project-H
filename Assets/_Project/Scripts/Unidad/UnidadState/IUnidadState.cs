using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnidadState
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
    
    public void OnEnter();
    public void OnUpdate();
    public void OnExit();
}
