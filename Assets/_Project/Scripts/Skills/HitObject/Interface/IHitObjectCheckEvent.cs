public interface IHitObjectCheckEvent
{
    public void Init(Unidad caster);
    public void Check(HitObject hitObject);
}
