using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaderBoard()
    {
        // open leaderboard
    }

    public void HowToPlay()
    {
        // open how to play
    }

    public void Quit()
    {
        Application.Quit();
    }
}
