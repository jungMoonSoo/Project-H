using UnityEngine;

public class DestroyFinishEvent: MonoBehaviour, ISkillEffectFinishEvent
{
    public void OnFinish(SkillEffectHandler handler)
    {
        Destroy(handler.gameObject);
    }
}