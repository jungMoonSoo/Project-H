using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    private ObjectPool<EffectSystem> effects;

    public void Init(ObjectPool<EffectSystem> effects)
    {
        this.effects = effects;
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    private void OnParticleSystemStopped() => effects.Enqueue(this);
}
