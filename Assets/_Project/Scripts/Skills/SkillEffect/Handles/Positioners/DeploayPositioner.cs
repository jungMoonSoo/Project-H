using UnityEngine;

public class DeploayPositioner: MonoBehaviour, ISkillEffectPositioner
{
    public void SetPosition(SkillEffectHandlerBase handler, Vector3 position)
    {
        handler.transform.position = position;
    }
}