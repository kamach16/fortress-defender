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
        priceText.text = price.ToString() + "$";
    }

    public void BuyTower()
    {
        if (gameManager.currentMoney >= price)
        {
            if(shopScreen.towerPlaceSpot.childCount > 0 && shopScreen.towerPlaceSpot.GetChild(0).name != towerPrefab.name) // if there is a defence object in selected tower spot and if defence object in selected tower spot is the same as we want to buy
            {
                Destroy(shopScreen.towerPlaceSpot.GetChild(0).gameObject);
                CreateTower();
                gameManager.SubtractMoney(price);
            }
            else if (shopScreen.towerPlaceSpot.childCount == 0)
            {
                CreateTower();
                gameManager.SubtractMoney(price);
            }
        }
    }

    private void CreateTower()
    {
        GameObject tower = Instantiate(towerPrefab, shopScreen.towerPlaceSpot);
        tower.name = towerPrefab.name;
    }
}
