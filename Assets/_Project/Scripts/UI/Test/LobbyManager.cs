using UnityEngine;
public class LobbyManager : MonoBehaviour
{
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

}
