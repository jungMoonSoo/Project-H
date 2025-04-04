using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresetUnitItem : MonoBehaviour
{
    [SerializeField] private TMP_Text unitName;
    [SerializeField] private TMP_Text unitLevel;

    [SerializeField] private Image icon;

    private PlayerUnitInfo info;

    public void SetInfo(PlayerUnitInfo info)
    {
        this.info = info;

        unitName.text = info.id.ToString();
        unitLevel.text = (info.level + 1).ToString();
    }
}
