using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public bool pause = false;
    public GameObject pauseMenu;
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    public void Play()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            pause = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            pause = false;
            Time.timeScale = 1;
        }
    }
}