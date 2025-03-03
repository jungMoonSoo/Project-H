using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonHandle : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    public void SetID(uint id)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => AllyUnitDeployment.Instance.UnitButton(id));

        text.text = id.ToString();
    }
}
