using UnityEngine.Pool;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    private IObjectPool<EffectSystem> effectPool;

    public void SetPool(IObjectPool<EffectSystem> pool) => effectPool = pool;

    public void SetActive(bool active) => gameObject.SetActive(active);

    private void OnParticleSystemStopped() => effectPool.Release(this);
}
