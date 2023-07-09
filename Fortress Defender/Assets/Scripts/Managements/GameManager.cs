using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public bool lost = false;
    [SerializeField] public int currentMoney;
    [SerializeField] private MoneyDisplay moneyDisplay;
    [SerializeField] private GameObject shopScreen;

    public delegate void Action();
    public static event Action OnLevelWin;
    public static event Action OnNewLevelStarted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
        Debug.Log("You lost the game");
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
