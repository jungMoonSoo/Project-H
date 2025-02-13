public interface ICheckHitable
{
    public HitObject HitObject { get; set; }

    public void Hit();
    public void Remove();
}
