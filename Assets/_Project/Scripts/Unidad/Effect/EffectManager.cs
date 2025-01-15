using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private EffectSystem effectObject;

    private ObjectPool<EffectSystem> effectObjects;

    private void Start()
    {
        effectObjects = new(effectObject)
        {
            OnEnqueue = OnEnqueue,
            OnDequeue = OnDequeue
        };
    }

    public void CreateDefaultEffects(int count)
    {
        for (int i = effectObjects.Count - 1; i < count; i++) effectObjects.CreateDefault(transform);
    }

    public EffectSystem GetEffect(Transform parent) => effectObjects.Dequeue(parent);

    private void OnEnqueue(EffectSystem effect)
    {
        if (this == null)
        {
            Destroy(effect);

            return;
        }

        effect.transform.SetParent(transform);
        effect.SetActive(false);
    }

    private void OnDequeue(EffectSystem effect)
    {
        effect.Init(effectObjects);
        effect.SetActive(true);
    }
}
