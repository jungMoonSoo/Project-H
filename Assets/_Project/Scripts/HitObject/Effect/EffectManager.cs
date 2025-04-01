using UnityEngine.Pool;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private EffectSystem effectPrefab;

    public IObjectPool<EffectSystem> EffectPool { get; private set; }

    private void Start() => EffectPool = new ObjectPool<EffectSystem>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

    public void CreateDefault(int count)
    {
        EffectSystem effect;

        for (int i = 0; i < count; i++)
        {
            effect = CreateObject();

            EffectPool.Release(effect);
        }
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
