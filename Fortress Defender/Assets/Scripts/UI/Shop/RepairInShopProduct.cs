using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RepairInShopProduct : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private float healthToAdd;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        priceText.text = price.ToString() + "$";
        nameText.text = "REPAIR +" + healthToAdd.ToString();
    }

    public void Repair()
    {
        if(gameManager.currentMoney >= price && playerHealth.health < playerHealth.maxHealth)
        {
            playerHealth.AddHealth(healthToAdd);
            healthText.text = "HEALTH: " + playerHealth.health.ToString() + "/" + playerHealth.maxHealth.ToString();
            gameManager.SubtractMoney(price);
        }
    }
}
