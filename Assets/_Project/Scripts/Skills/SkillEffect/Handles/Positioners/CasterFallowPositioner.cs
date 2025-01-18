using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterFallowPositioner : MonoBehaviour, ISkillEffectPositioner
{
    private SkillEffectHandlerBase handler;

    public void SetPosition(SkillEffectHandlerBase handler, Vector2 position)
    {
        this.handler = handler;

        StartCoroutine(CasterFallow());
    }

    private IEnumerator CasterFallow()
    {
        while (true)
        {
            if (handler.Caster == null) break;

            handler.transform.position = handler.Caster.transform.position;

            yield return null;
        }

        Destroy(handler.gameObject);
    }
}
