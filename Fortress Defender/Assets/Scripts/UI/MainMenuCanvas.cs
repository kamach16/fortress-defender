using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayScreen;
    [SerializeField] private GameObject leaderboardScreen;

    [Header("Initial Warning")]
    [SerializeField] private TextMeshProUGUI countingText;
    [SerializeField] private float timeToTurnOffWarning;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private CanvasGroup fade;
    [SerializeField] private AudioSource backgroundMusicAudioSource;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("InitialWarningShowed"))
        {
            fade.gameObject.SetActive(false);
            backgroundMusicAudioSource.Play();
        }
    }

    private void Update()
    {
        AnimateInitialWarning();
    }

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

    private void AnimateInitialWarning()
    {
        if (PlayerPrefs.HasKey("InitialWarningShowed")) return;

        timeToTurnOffWarning = Mathf.Max(timeToTurnOffWarning - Time.deltaTime, 0);

        countingText.text = $"{(int)timeToTurnOffWarning}";

        if(timeToTurnOffWarning <= 0)
        {
            fade.alpha -= Time.deltaTime / fadeOutTime;

            if (!backgroundMusicAudioSource.isPlaying) backgroundMusicAudioSource.Play();
            if (fade.alpha <= 0) { fade.gameObject.SetActive(false); PlayerPrefs.SetInt("InitialWarningShowed", 1); }
        }
    }
}
