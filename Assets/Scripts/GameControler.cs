using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameUI;
    public GameObject gameOverUI;
    public GameObject nextMapUI;
    public GameObject winGameUI;
    public Text mapLevelText;
    public GameObject mapLevel1, mapLevel2;
    private int levelGame;
    private GameObject mapObject;

    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    // Update is called once per frame
    void Update()
    {}

    public void OnClickPauseButton()
    {
        pauseMenu.SetActive(true);
        gameOverUI.SetActive(false);
        gameUI.SetActive(false);
        nextMapUI.SetActive(false);
        winGameUI.SetActive(false);
        Time.timeScale = 0f;

    }

    public void GameOver()
    {
        if (mapObject != null)
            Destroy(mapObject);
        pauseMenu.SetActive(false);
        gameUI.SetActive(false);
        nextMapUI.SetActive(false);
        winGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        if (mapObject != null)
            Destroy(mapObject);
        pauseMenu.SetActive(false);
        gameOverUI.SetActive(false);
        nextMapUI.SetActive(false);
        gameUI.SetActive(false);
        winGameUI.SetActive(true);
    }

    public void NextMap()
    {
        if (mapObject != null)
            Destroy(mapObject);
        levelGame++;
        mapLevelText.text = "Map " + levelGame.ToString();
        nextMapUI.SetActive(true);
        StartCoroutine(nextMapWait());
    }

    public void Play()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameOverUI.SetActive(false);
        nextMapUI.SetActive(false);
        winGameUI.SetActive(false);
        gameUI.SetActive(true);
        levelGame = 1;
        mapLevelText.text = "Map " + levelGame.ToString();
        nextMapUI.SetActive(true);
        StartCoroutine(nextMapWait());
    }

    IEnumerator nextMapWait() {
        int count = 0;
        while (true)
        {
            if (count < 5)
                yield return new WaitForSeconds(1);
            nextMapUI.SetActive(false);
            StartMap();
            yield break;
        }
    }

    public void StartMap() {
        if (levelGame == 1)
        {
            mapObject = Instantiate(mapLevel1);
        }
        else if (levelGame == 2)
        {
            mapObject = Instantiate(mapLevel2);
        } 
        else
        {
            WinGame();
        }
        GameObject numberEnemy = GameObject.FindGameObjectWithTag("numberEnemy");
        numberEnemy.GetComponent<Text>().text = GameObject.FindGameObjectsWithTag("enemy").Length.ToString();
    }
}
