using TMPro;
using UnityEngine;

public class UnitStorageItem : MonoBehaviour
{
    [SerializeField] private TMP_Text id;

    public void SetID(string id) => this.id.text = id;
}
