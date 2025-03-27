using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private UnitStorageManager unitStorage;

    /// <summary>
    /// Game Scene 전환
    /// </summary>
    public void ChangeScene()
    {
        LoadingSceneController.LoadScene("InGame");
    }

    /// <summary>
    /// Game Scene 전환
    /// </summary>
    public void GoGacha()
    {
        LoadingSceneController.LoadScene("Gacha");
    }

    public void ToggleUnitStorage(bool active) => unitStorage.SetActive(active);
}
