using UnityEngine;
using UnityEngine.Pool;

public class PresetButtonManager : MonoBehaviour
{
    [SerializeField] private PresetButtonItem itemPrefab;
    [SerializeField] private Transform spawnTrans;

    public IObjectPool<PresetButtonItem> ItemPool { get; private set; }

    public void Init() => ItemPool = new ObjectPool<PresetButtonItem>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

    public void SetItem(int index) => ItemPool.Get().SetIndex(index);

    #region 풀링
    private PresetButtonItem CreateObject() => Instantiate(itemPrefab, spawnTrans);

    private void OnGetObject(PresetButtonItem item) => item.gameObject.SetActive(true);

    private void OnReleseObject(PresetButtonItem item) => item.gameObject.SetActive(false);

    private void OnDestroyObject(PresetButtonItem item) => Destroy(item.gameObject);
    #endregion
}
