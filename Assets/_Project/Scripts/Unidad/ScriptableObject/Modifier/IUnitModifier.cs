public interface IUnitModifier
{
    public int Id { get; }

    public float Count { get; }

    public StatusManager Status { get; }

    public void Apply(StatusManager status, float count);
    public void Check(float count);
    public void Remove();
}
