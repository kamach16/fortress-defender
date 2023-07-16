using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayScreen;
    [SerializeField] private GameObject leaderboardScreen;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaderBoard()
    {
        leaderboardScreen.SetActive(true);
    }

    public void OpenHowToPlay()
    {
        howToPlayScreen.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
