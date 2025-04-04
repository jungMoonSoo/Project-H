using UnityEngine;
using UnityEngine.Pool;

public class PresetButtonManager : MonoBehaviour
{
    [SerializeField] private PresetButtonItem itemPrefab;
    [SerializeField] private Transform spawnTrans;

    private PresetManager presetManager;

    public IObjectPool<PresetButtonItem> ItemPool { get; private set; }

    public void Init(PresetManager presetManager)
    {
        this.presetManager = presetManager;

        ItemPool = new ObjectPool<PresetButtonItem>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

        for (int i = 0; i < PlayerManager.Instance.presets.Count; i++) SetItem(i);
    }

    public void SetItem(int index) => ItemPool.Get().SetInfo(index, presetManager);

    #region 풀링
    private PresetButtonItem CreateObject() => Instantiate(itemPrefab, spawnTrans);

    private void OnGetObject(PresetButtonItem item) => item.gameObject.SetActive(true);

    private void OnReleseObject(PresetButtonItem item) => item.gameObject.SetActive(false);

    private void OnDestroyObject(PresetButtonItem item) => Destroy(item.gameObject);
    #endregion
}
