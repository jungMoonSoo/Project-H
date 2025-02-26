using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectManager : MonoBehaviour
{
    [SerializeField] private HitObjectBase hitObject;

    private ObjectPool<HitObjectBase> hitObjects;

    private void Start()
    {
        hitObjects = new(hitObject)
        {
            OnEnqueue = OnEnqueue,
            OnDequeue = OnDequeue
        };
    }

    public void CreateDefaultHitObjects(int count)
    {
        for (int i = hitObjects.Count - 1; i < count; i++) hitObjects.CreateDefault(transform);
    }

    public HitObjectBase GetHitObject(Transform parent) => hitObjects.Dequeue(parent);

    private void OnEnqueue(HitObjectBase hitObject)
    {
        hitObject.transform.SetParent(transform);
        hitObject.gameObject.SetActive(false);
    }

    private void OnDequeue(HitObjectBase hitObject)
    {
        hitObject.SetPool(hitObjects);
        hitObject.gameObject.SetActive(true);
    }
}
