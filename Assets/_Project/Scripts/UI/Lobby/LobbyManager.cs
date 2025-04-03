using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private UnitStorageManager unitStorage;

    /// <summary>
    /// Game Scene 전환
    /// </summary>
    public void ChangeScene(string name)
    {
        LoadingSceneController.LoadScene(name);
    }

    public void ToggleUnitStorage(bool active) => unitStorage.SetActive(active);
}
