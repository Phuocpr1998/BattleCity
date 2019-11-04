using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject loadingScreen, loadingIcon;
    public Text loadingText;

    public void OnClickButtonPlay()
    {
        StartCoroutine(LoadingPlayScene());
    }

    public void OnClickButtonOptions()
    {
        SceneManager.LoadScene("OptionsScene", LoadSceneMode.Additive);
    }

    public void OnClickButtonQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public IEnumerator LoadingPlayScene()
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync("PlayScene");
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                loadingText.text = "Press any key to continue";
                loadingIcon.SetActive(false);
                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                    Time.timeScale = 1f;
                }
            }
            yield return null;
        }
    }

}
