using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPauseButton()
    {
        pauseMenu.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
    }
}
