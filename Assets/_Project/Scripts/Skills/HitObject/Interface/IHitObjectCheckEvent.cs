public interface IHitObjectCheckEvent
{
    public HitObjectBase HitObject { get; set; }

    public void Check();
}
