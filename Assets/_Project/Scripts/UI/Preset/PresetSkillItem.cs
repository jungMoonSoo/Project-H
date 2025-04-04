using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresetSkillItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    [SerializeField] private TMP_Text idText;

    private int skillIndex;
    private PresetManager presetManager;

    public void SetInfo(int id) => idText.text = id == -1 ? "" : id.ToString();

    public void SetEvent(int skillIndex, PresetManager presetManager)
    {
        this.skillIndex = skillIndex;
        this.presetManager = presetManager;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() => SetInfo(presetManager.CreateUnit(skillIndex));
}
