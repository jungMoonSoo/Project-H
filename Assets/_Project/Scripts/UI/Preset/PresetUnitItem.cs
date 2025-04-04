using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresetUnitItem : MonoBehaviour
{
    [SerializeField] private TMP_Text unitName;
    [SerializeField] private TMP_Text unitLevel;

    [SerializeField] private Image icon;

    [SerializeField] private Button selectButton;

    [SerializeField] private TMP_Text deployButtonText;
    [SerializeField] private Button deployButton;

    private PlayerUnitInfo info;
    private PresetUnitManager manager;

    public uint ID => info.id;

    public void SetInfo(PlayerUnitInfo info, PresetUnitManager manager)
    {
        this.info = info;
        this.manager = manager;

        unitName.text = info.id.ToString();
        unitLevel.text = (info.level + 1).ToString();

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnSelect);

        deployButton.onClick.RemoveAllListeners();
        deployButton.onClick.AddListener(OnDeploy);

        deployButton.gameObject.SetActive(false);
    }

    private void OnSelect()
    {
        if (deployButton.gameObject.activeSelf) deployButton.gameObject.SetActive(false);
        else if (manager.SelectUnitID == (int)ID) manager.SelectUnitID = -1;
        else deployButton.gameObject.SetActive(true);

        SetDeployText();
    }

    private void OnDeploy()
    {
        OnSelect();

        if (manager.presetManager.CheckSpawn((int)ID))
        {
            manager.SelectUnitID = -1;
            manager.presetManager.RemoveUnit((int)ID);
        }
        else manager.SelectUnitID = (int)ID;
    }

    private void SetDeployText()
    {
        if (manager.presetManager.CheckSpawn((int)ID)) deployButtonText.text = "Remove";
        else deployButtonText.text = "Add";
    }
}
