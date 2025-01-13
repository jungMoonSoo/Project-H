using UnityEngine;

public class DestroyFinishEvent: MonoBehaviour, ISkillEffectFinishEvent
{
    public void OnFinish(SkillEffectHandlerBase handler)
    {
        Destroy(handler.gameObject);
    }
}