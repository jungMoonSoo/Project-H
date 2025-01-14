public interface IUnidadTargeting
{
    public bool TryGetTargets(out Unidad[] targets, UnitType type, EllipseCollider collider, int count);
}