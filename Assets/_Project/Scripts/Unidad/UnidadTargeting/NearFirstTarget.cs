using System.Collections.Generic;

public class NearFirstTarget : IUnidadTargeting
{
    public Unidad[] GetTargets(UnitType type, EllipseCollider collider, int count)
    {
        List<Unidad> result = new(UnidadManager.Instance.GetUnidads(type, TargetType.They));

        result.Sort((x, y) => collider.OnEllipseDepth(x.unitCollider) > collider.OnEllipseDepth(y.unitCollider) ? 1 : -1);
        
        return result.GetRange(0, count).ToArray();
    }
}