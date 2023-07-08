using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMachineDefenceObject : MonoBehaviour
{
    [SerializeField] private int moneyToAdd;
    [SerializeField] private float timeBetweenGettingMoney;

    private GameManager gameManager;

    private void Awake()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenGettingMoney);
            gameManager.AddMoney(moneyToAdd);
        }
    }
}
