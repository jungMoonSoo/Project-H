using UnityEngine;

public class DestroyFinishEvent: MonoBehaviour, IHitObjectFinishEvent
{
    public void OnFinish(HitObjectBase handler)
    {
        Destroy(handler.gameObject);
    }
}