using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventHandler : MonoBehaviour
{
    public List<UnityEvent> events;

    public void OnEvent(int index) => events[index].Invoke();
}
