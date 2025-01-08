public interface IUnidadTargeting
{
    public Unidad[] GetTargets(UnitType type, EllipseCollider collider, int count);
}