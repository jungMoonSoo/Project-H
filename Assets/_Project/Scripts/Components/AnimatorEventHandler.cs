using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventHandler : MonoBehaviour
{
    public Action OnTriggerEvent = null;
    public Action OnFinishEvent = null;
    
    public void OnTrigger()
    {
        OnTriggerEvent?.Invoke();
    }

    public void OnFinish()
    {
        OnFinishEvent?.Invoke();
    }
}
