public interface IHitObjectTriggerEvent
{
    public void Init(Unidad caster);
    public void OnTrigger(HitObject hitObject);
}