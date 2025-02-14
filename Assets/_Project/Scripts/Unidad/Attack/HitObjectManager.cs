using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectManager : MonoBehaviour
{
    [SerializeField] private HitObject hitObject;

    private ObjectPool<HitObject> hitObjects;

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

    public HitObject GetHitObject(Transform parent) => hitObjects.Dequeue(parent);

    private void OnEnqueue(HitObject hitObject)
    {
        hitObject.transform.SetParent(transform);
        hitObject.gameObject.SetActive(false);
    }

    private void OnDequeue(HitObject hitObject)
    {
        hitObject.Init(hitObjects);
        hitObject.gameObject.SetActive(true);
    }
}
