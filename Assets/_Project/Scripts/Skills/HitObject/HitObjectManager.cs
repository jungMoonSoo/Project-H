using UnityEngine.Pool;
using UnityEngine;

public class HitObjectManager : MonoBehaviour
{
    [SerializeField] private HitObject hitObjectPrefab;

    public IObjectPool<HitObject> HitObjectPool { get; private set; }

    private void Start() => HitObjectPool = new ObjectPool<HitObject>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

    private HitObject CreateObject()
    {
        HitObject hitObject = Instantiate(hitObjectPrefab);

        hitObject.SetPool(HitObjectPool);

        return hitObject;
    }

    private void OnGetObject(HitObject hitObject) => hitObject.gameObject.SetActive(true);

    private void OnReleseObject(HitObject hitObject)
    {
        hitObject.transform.SetParent(transform);
        hitObject.gameObject.SetActive(false);
    }

    private void OnDestroyObject(HitObject hitObject) => Destroy(hitObject.gameObject);
}
