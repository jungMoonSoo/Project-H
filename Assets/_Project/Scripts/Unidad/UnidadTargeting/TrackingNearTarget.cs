using System.Collections.Generic;

public class TrackingNearTarget : ITrackingSystem
{
    public bool TryGetTargets(out Unidad[] targets, UnitType type, EllipseCollider collider)
    {
        List<Unidad> result = new(UnidadManager.Instance.GetUnidads(type, TargetType.They));

        result.Sort((x, y) => collider.OnEllipseDepth(x.unitCollider) > collider.OnEllipseDepth(y.unitCollider) ? 1 : -1);

        targets = null;

        if (result.Count == 0) return false;

        targets = result.ToArray();

        return true;
    }
}