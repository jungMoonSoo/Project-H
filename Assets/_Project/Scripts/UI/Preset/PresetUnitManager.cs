using UnityEngine;
using UnityEngine.Pool;

public class PresetUnitManager : MonoBehaviour
{
    [SerializeField] private PresetUnitItem itemPrefab;
    [SerializeField] private Transform spawnTrans;

    public PresetManager presetManager;

    public int SelectUnitID { get; set; }

    public IObjectPool<PresetUnitItem> ItemPool { get; private set; }

    public void Init(PresetManager presetManager)
    {
        this.presetManager = presetManager;

        ItemPool = new ObjectPool<PresetUnitItem>(CreateObject, OnGetObject, OnReleseObject, OnDestroyObject);

        foreach (PlayerUnitInfo unitInfo in PlayerManager.Instance.units.Values) SetItem(unitInfo);
    }

    public void SetItem(PlayerUnitInfo info) => ItemPool.Get().SetInfo(info, this);

    #region 풀링
    private PresetUnitItem CreateObject() => Instantiate(itemPrefab, spawnTrans);

    private void OnGetObject(PresetUnitItem item) => item.gameObject.SetActive(true);

    private void OnReleseObject(PresetUnitItem item) => item.gameObject.SetActive(false);

    private void OnDestroyObject(PresetUnitItem item) => Destroy(item.gameObject);
    #endregion
}
