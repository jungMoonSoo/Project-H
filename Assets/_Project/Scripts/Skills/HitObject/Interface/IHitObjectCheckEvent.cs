public interface IHitObjectCheckEvent
{
    public void Init(Unidad caster);
    public void OnEvent(HitObject hitObject);
}
