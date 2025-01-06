public interface IUnitModifier
{
    public int Id { get; }

    public int Count { get; }

    public void Add(Unidad unidad);
    public void Remove(Unidad unidad);

    public int Cycle(Unidad unidad);
}
