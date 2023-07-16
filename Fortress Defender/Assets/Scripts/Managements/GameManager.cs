using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public bool lost = false;
    [SerializeField] private bool gamePaused = false;
    [SerializeField] public int currentMoney;
    [SerializeField] private MoneyDisplay moneyDisplay;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject pauseScreen;

    public delegate void Action();
    public static event Action OnLevelWin;
    public static event Action OnNewLevelStarted;
    public static event Action OnDefeat;
    public static event Action OnPause;
    public static event Action OnUnpause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) PauseOrUnpauseGame();
    }

    public void PauseOrUnpauseGame()
    {
        if (gamePaused) // unpause
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            if (OnUnpause != null) OnUnpause();
        }
        else // pause
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            if (OnPause != null) OnPause();
        }

        gamePaused = !gamePaused;
    }

    public void WinLevel()
    {
        if (lost) return;

        shopScreen.SetActive(true);

        if (OnLevelWin != null) OnLevelWin();
    }

    public void ContinueGame()
    {
        shopScreen.SetActive(false);

        if (OnNewLevelStarted != null) OnNewLevelStarted();
    }

    public void LoseGame()
    {
        lost = true;
        defeatScreen.SetActive(true);

        if (OnDefeat != null) OnDefeat();
    }

    public void AddMoney(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
        moneyDisplay.UpdateMoneyText(currentMoney);
    }

    public void SubtractMoney(int moneyToSubtract)
    {
        currentMoney -= moneyToSubtract;
        moneyDisplay.UpdateMoneyText(currentMoney);
    }
}
