using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoldierInShopProduct : MonoBehaviour
{
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private Transform soldiersContainer;
    [SerializeField] private int price;
    [SerializeField] private int billPerDay;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI soldiersAmountText;
    [SerializeField] private TextMeshProUGUI soldierBillText;
    [SerializeField] private GameManager gameManager;

    private int soldiersAmount = 0;

    private void Start()
    {
        SetVariables();
    }

    private void OnEnable()
    {
        PayBill();
    }

    private void PayBill()
    {
        gameManager.SubtractMoney(billPerDay * soldiersAmount);
    }

    private void SetVariables()
    {
        priceText.text = price.ToString() + "$";
        soldierBillText.text = "X1  -" + billPerDay + "$/DAY";
    }

    public void BuySoldier()
    {
        if (gameManager.currentMoney >= price)
        {
            Instantiate(soldierPrefab, soldiersContainer);
            gameManager.SubtractMoney(price);
            soldiersAmount++;
            soldiersAmountText.text = "SOLDIERS: " + soldiersAmount;
        }
    }
}