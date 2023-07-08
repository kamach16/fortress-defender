using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldInShopProduct : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private float shieldToAdd;
    [SerializeField] private TextMeshProUGUI shieldText;
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
        nameText.text = "SHIELD +" + shieldToAdd.ToString();
    }

    public void AddShield()
    {
        if (gameManager.currentMoney >= price && playerHealth.shield < playerHealth.maxShield)
        {
            playerHealth.AddShield(shieldToAdd);
            shieldText.text = "SHIELD: " + playerHealth.shield.ToString() + "/" + playerHealth.maxShield.ToString();
            gameManager.SubtractMoney(price);
        }
    }
}
