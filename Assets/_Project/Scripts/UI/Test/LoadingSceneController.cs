using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : Singleton<LoadingSceneController>
{
    static string nextScene;

    [SerializeField] Slider progressBar;

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingTest");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.value = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}