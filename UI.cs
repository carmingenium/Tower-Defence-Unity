using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Play()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}