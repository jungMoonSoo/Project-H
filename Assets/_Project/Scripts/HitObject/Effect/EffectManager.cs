using UnityEngine.Pool;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private EffectSystem effectPrefab;
    [SerializeField] private int defaultCreateCount = 0;

    public IObjectPool<EffectSystem> EffectPool { get; private set; }

    private void Start() => EffectPool = new ObjectPool<EffectSystem>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

    public void CreateDefault()
    {
        if (effectPrefab == null) return;

        for (int i = 0; i < defaultCreateCount; i++) EffectPool.Release(CreateObject());
    }

    private EffectSystem CreateObject()
    {
        EffectSystem effectSystem = Instantiate(effectPrefab);

        effectSystem.SetPool(EffectPool);

        return effectSystem;
    }

    private void OnGetObject(EffectSystem effectSystem) => effectSystem.gameObject.SetActive(true);

    private void OnReleseObject(EffectSystem effectSystem)
    {
        effectSystem.transform.SetParent(transform);
        effectSystem.gameObject.SetActive(false);
    }

    private void OnDestroyObject(EffectSystem effectSystem) => Destroy(effectSystem.gameObject);
}
