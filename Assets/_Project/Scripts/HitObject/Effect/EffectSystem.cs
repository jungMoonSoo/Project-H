using UnityEngine.Pool;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    private IObjectPool<EffectSystem> effectPool;

    public void SetPool(IObjectPool<EffectSystem> pool) => effectPool = pool;

    public void SetActive(bool active) => gameObject.SetActive(active);

    /// <summary>
    /// 파티클 종료시 자동 콜백, 풀링
    /// </summary>
    private void OnParticleSystemStopped() => effectPool.Release(this);
}
