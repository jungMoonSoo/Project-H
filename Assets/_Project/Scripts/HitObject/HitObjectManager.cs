using UnityEngine.Pool;
using UnityEngine;

public class HitObjectManager : MonoBehaviour
{
    [SerializeField] private HitObject hitObjectPrefab;
    [SerializeField] private int defaultCreateCount = 0;

    public IObjectPool<HitObject> HitObjectPool { get; private set; }

    private void Start() => HitObjectPool = new ObjectPool<HitObject>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

    /// <summary>
    /// Spawn 될 오브젝트의 Prefab 변경
    /// </summary>
    /// <param name="hitObjectPrefab">변경 할 오브젝트</param>
    public void SetPrefab(HitObject hitObjectPrefab) => this.hitObjectPrefab = hitObjectPrefab;

    /// <summary>
    /// defaultCreateCount만큼 오브젝트 생성
    /// </summary>
    public void CreateDefault()
    {
        if (hitObjectPrefab == null) return;

        for (int i = 0; i < defaultCreateCount; i++) HitObjectPool.Release(CreateObject());
    }

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
