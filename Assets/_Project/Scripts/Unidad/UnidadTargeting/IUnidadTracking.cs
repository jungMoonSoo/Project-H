public interface IUnidadTracking
{
    public Unidad[] Targets { get; }

    public void SetType(TrackingType type);

    public UnitState Check(Unidad unidad);
    public void CreateHitObject(Unidad unidad);
}
