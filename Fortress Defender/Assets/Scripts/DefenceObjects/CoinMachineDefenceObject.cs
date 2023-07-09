using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMachineDefenceObject : MonoBehaviour
{
    [SerializeField] private int moneyToAdd;
    [SerializeField] private float timeBetweenGettingMoney;
    [SerializeField] private bool canAddMoney;

    private GameManager gameManager;

    private void OnEnable()
    {
        GameManager.OnLevelWin += StopAddingMoney;
        GameManager.OnNewLevelStarted += AllowAddingMoney;
    }

    private void OnDisable()
    {
        GameManager.OnLevelWin -= StopAddingMoney;
        GameManager.OnNewLevelStarted -= AllowAddingMoney;
    }

    private void Awake()
    {
        SetVariables();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenGettingMoney);
            if (canAddMoney) gameManager.AddMoney(moneyToAdd);
        }
    }

    private void SetVariables()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void StopAddingMoney()
    {
        canAddMoney = false;
    }

    private void AllowAddingMoney()
    {
        canAddMoney = true;
    }
}
