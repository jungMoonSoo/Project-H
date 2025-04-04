using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresetButtonItem : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button button;

    private PresetManager manager;
    private int index;

    public void SetInfo(int index, PresetManager manager)
    {
        this.index = index;
        this.manager = manager;

        title.text = $"{index + 1}팀";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() => manager.DeployUnit(index);
}
