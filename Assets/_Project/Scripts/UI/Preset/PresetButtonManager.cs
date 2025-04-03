using UnityEngine;
using UnityEngine.Pool;

public class PresetButtonManager : MonoBehaviour
{
    [SerializeField] private PresetButtonItem itemPrefab;
    [SerializeField] private Transform spawnTrans;

    [SerializeField] private int count;

    public IObjectPool<PresetButtonItem> ItemPool { get; private set; }

    private void Start()
    {
        ItemPool = new ObjectPool<PresetButtonItem>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

        SetItems();
    }

    private void SetItems()
    {
        if (ItemPool == null) Start();

        PresetButtonItem item;

        for (int i = 0; i < count; i++)
        {
            item = ItemPool.Get();
            item.SetIndex(i);
        }
    }

    #region 풀링
    private PresetButtonItem CreateObject() => Instantiate(itemPrefab, spawnTrans);

    private void OnGetObject(PresetButtonItem item) => item.gameObject.SetActive(true);

    private void OnReleseObject(PresetButtonItem item) => item.gameObject.SetActive(false);

    private void OnDestroyObject(PresetButtonItem item) => Destroy(item.gameObject);
    #endregion
}
