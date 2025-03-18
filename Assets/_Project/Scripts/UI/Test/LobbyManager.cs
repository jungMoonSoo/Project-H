using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public void ChangeScene()
    {
        LoadingSceneController.LoadScene("InGame");
    }

    public void GoGacha()
    {
        LoadingSceneController.LoadScene("Gacha");
    }

}
