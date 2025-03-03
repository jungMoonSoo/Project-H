using UnityEngine;

public class Uniquer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        LoadingSceneController.LoadScene("Lobby");
    }
}
