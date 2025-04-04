using UnityEngine;

public class PresetSkillManager : MonoBehaviour
{
    [SerializeField] private PresetSkillItem[] items;

    private PresetManager presetManager;

    public void Init(PresetManager presetManager)
    {
        this.presetManager = presetManager;

        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetEvent(i, presetManager);
            items[i].SetInfo(-1);
        }
    }

    public void SetInfo(int id, int index) => items[index].SetInfo(id);

    public void Clear()
    {
        for (int i = 0; i < items.Length; i++) items[i].SetInfo(-1);
    }
}
