public interface IStatusEffect
{
    public int Id { get; }

    public int Count { get; }

    public StatusManager Status { get; }

    public void Apply(StatusManager _status);
    public void Check(int _duration);
    public void Remove();
}
