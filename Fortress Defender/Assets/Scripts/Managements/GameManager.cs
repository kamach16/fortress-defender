using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public bool lost = false;
    [SerializeField] private int currentMoney;
    [SerializeField] private MoneyDisplay moneyDisplay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
