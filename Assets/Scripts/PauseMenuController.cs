using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;
    public GameObject loadingScreen, loadingIcon;
    public Text textLoading;

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
    }

    public void OpenOptionMenu()
    {
        SceneManager.LoadScene("OptionsScene", LoadSceneMode.Additive);
    }

    public void OpenMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    public IEnumerator LoadMainMenu()
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync("MenuGame");
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                textLoading.text = "Press any key to continue";
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
