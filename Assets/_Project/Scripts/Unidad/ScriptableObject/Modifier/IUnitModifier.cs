public interface IUnitModifier
{
    public int Id { get; }

    public float Count { get; }

    public void Add(Unidad unidad);
    public void Tick(Unidad unidad);
    public void Remove(Unidad unidad);
}
