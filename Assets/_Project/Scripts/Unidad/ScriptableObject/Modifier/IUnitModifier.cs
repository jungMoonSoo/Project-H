public interface IUnitModifier
{
    public int Id { get; }

    public int CycleCount { get; }

    public void Add(Unidad unidad);
    public void Remove(Unidad unidad);

    public int Cycle(Unidad unidad);
}
