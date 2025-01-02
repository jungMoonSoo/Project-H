using UnityEngine;

public class DeploayPositioner: MonoBehaviour, ISkillEffectPositioner
{
    public void SetPosition(SkillEffectHandler handler, Vector2 position)
    {
        handler.transform.position = position;
    }
}