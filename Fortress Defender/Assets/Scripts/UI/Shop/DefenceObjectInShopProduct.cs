using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenceObjectInShopProduct : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private int price;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private ShopScreen shopScreen;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        priceText.text = price.ToString();
    }

    public void BuyTower()
    {
        if (gameManager.currentMoney >= price)
        {
            Instantiate(towerPrefab, shopScreen.towerPlaceSpot);
        }
    }
}
