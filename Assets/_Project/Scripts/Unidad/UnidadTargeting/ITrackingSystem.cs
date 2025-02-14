public interface ITrackingSystem
{
    public bool TryGetTargets(out Unidad[] targets, UnitType type, EllipseCollider collider);
}