using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : PlayerControl
{
    public Scene scene;
    public int buildIndex;

    public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
    }
    new public void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;
        if (buildIndex == 0)
        {
            Time.timeScale = 1;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("JulbScene");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void LoadShop()
    {
        SceneManager.LoadScene("Kauppa");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
