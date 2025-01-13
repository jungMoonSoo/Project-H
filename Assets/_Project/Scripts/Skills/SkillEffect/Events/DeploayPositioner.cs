using UnityEngine;

public class DeploayPositioner: MonoBehaviour, ISkillEffectPositioner
{
    public void SetPosition(SkillEffectHandlerBase handler, Vector2 position)
    {
        handler.transform.position = position;
    }
}