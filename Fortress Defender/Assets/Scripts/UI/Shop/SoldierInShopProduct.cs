using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoldierInShopProduct : MonoBehaviour
{
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private Transform soldiersContainer;
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        priceText.text = price.ToString() + "$";
    }

    public void BuySoldier()
    {
        if (gameManager.currentMoney >= price)
        {
            Instantiate(soldierPrefab, soldiersContainer);
            gameManager.SubtractMoney(price);
        }
    }
}
