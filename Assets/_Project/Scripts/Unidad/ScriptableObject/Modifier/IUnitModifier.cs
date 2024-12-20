public interface IUnitModifier
{
    public int Id { get; }

    public int Count { get; }

    public StatusManager Status { get; }

    public void Apply(StatusManager status, int time);
    public void Check(int duration);
    public void Remove();
}
