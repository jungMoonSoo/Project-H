public interface IHitObjectCreateEvent
{
    public void Init(Unidad caster);
    public void OnCreate(HitObject hitObject);
}