using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventHandler : MonoBehaviour
{
    public Action OnCallbackEvent = null;
    
    public void OnEvent()
    {
        OnCallbackEvent?.Invoke();
    }
}
