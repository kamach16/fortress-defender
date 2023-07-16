using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wavesText;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject leaderboardScreen;

    private void OnEnable()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        wavesText.text = "WAVES: " + enemySpawner.waveNumber;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Leaderboard()
    {
        leaderboardScreen.SetActive(true);
    }
}
