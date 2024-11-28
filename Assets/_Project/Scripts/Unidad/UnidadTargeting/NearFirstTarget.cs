using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NearFirstTarget: MonoBehaviour, IUnidadTargeting
{
    public Unidad[] GetTargets(NewEllipseCollider collider, int count)
    {
        List<Unidad> result = GameObject.FindObjectsOfType<Unidad>().Where(x => collider.OnEllipseEnter(x.unitCollider)).ToList();
        result.Sort((x, y) => collider.OnEllipseDepth(x.unitCollider) > collider.OnEllipseDepth(y.unitCollider) ? 1 : -1);
        
        return result.GetRange(0, count).ToArray();
    }
}