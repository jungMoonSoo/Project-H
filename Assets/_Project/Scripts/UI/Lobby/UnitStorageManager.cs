using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnitStorageManager : MonoBehaviour
{
    [SerializeField] private UnitStorageItem itemPrefab;
    [SerializeField] private Transform storageTrans;

    public IObjectPool<UnitStorageItem> ItemPool { get; private set; }

    private readonly List<UnitStorageItem> items = new();

    private void Start() => ItemPool = new ObjectPool<UnitStorageItem>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);

        if (active) SetItems();
        else ClearItems();
    }

    private void SetItems()
    {
        if (ItemPool == null) Start();

        UnitStorageItem item;

        foreach (uint id in PlayerManager.Instance.units)
        {
            item = ItemPool.Get();
            item.SetID(id.ToString());
        }
    }

    private void ClearItems()
    {
        for (int i = items.Count - 1; i >= 0; i--) ItemPool.Release(items[i]);
    }

    #region 풀링
    private UnitStorageItem CreateObject()
    {
        UnitStorageItem item = Instantiate(itemPrefab, storageTrans);

        items.Add(item);

        return item;
    }

    private void OnGetObject(UnitStorageItem item) => item.gameObject.SetActive(true);

    private void OnReleseObject(UnitStorageItem item) => item.gameObject.SetActive(false);

    private void OnDestroyObject(UnitStorageItem item) => Destroy(item.gameObject);
    #endregion
}
