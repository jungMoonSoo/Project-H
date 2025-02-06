using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TrackingPositioner: MonoBehaviour, ISkillEffectPositioner
{
    public void SetPosition(SkillEffectHandlerBase handler, Vector3 position)
    {
        List<Unidad> targets = UnidadManager.Instance.GetUnidads(handler.Caster.Owner, handler.TargetType);
        float minRange = float.MaxValue;
        Unidad target = null;

        foreach (Unidad unidad in targets)
        {
            Vector3 direction = unidad.transform.position - position;
            float directionRange = direction.magnitude;

            if (directionRange < minRange)
            {
                target = unidad;
                minRange = directionRange;
            }
        }
        
        
        if (target is not null)
        {
            _ = Tracking(handler, target);
        }
        else
        {
            Destroy(handler.gameObject);
        }
    }

    private static async Task Tracking(SkillEffectHandlerBase handler, Unidad target)
    {
        while (true)
        {
            handler.transform.position = target.transform.position;
            
            await Task.Yield();
        }
    }
}