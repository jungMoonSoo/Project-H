using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadTest : MonoBehaviour
{
    /// <summary>
    /// Scene 전환이였던것(Scene 이름이 바뀌여서 쓸모없어짐)
    /// </summary>
    public void ChangeScene()
    {
        LoadingSceneController.LoadScene("main");
    }
}
