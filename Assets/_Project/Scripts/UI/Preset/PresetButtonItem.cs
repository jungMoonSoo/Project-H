using TMPro;
using UnityEngine;

public class PresetButtonItem : MonoBehaviour
{
    [SerializeField] private TMP_Text title;

    private int index;

    public void SetIndex(int index)
    {
        this.index = index;

        title.text = $"{index + 1}팀";
    }

    public void OnEvent()
    {

    }
}
