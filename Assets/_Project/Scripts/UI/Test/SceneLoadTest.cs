using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadTest : MonoBehaviour
{
    public void ChangeScene()
    {
        LoadingSceneController.LoadScene("main");
    }
}
