public interface IUnidadAttack
{
    public Unidad[] Targets { get; }

    public void SetType(UnidadTargetingType type);

    public UnitState Check(Unidad unidad);
    public void CreateHitObject(Unidad unidad);
}
