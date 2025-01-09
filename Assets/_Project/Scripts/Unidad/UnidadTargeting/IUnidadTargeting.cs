public interface IUnidadTargeting
{
    public Unidad[] GetTargets(UnitType type, EllipseCollider collider, int count);
    public void OnEvent(Unidad unidad, Unidad target);
}