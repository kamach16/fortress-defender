using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldText;

    private void OnEnable()
    {
        UpdateTexts();
    }

    public void ContinueGame()
    {
        gameManager.ContinueGame();
    }

    private void UpdateTexts()
    {
        healthText.text = "HEALTH: " + playerHealth.health.ToString() + "/" + playerHealth.maxHealth.ToString();
        shieldText.text = "SHIELD: " + playerHealth.shield.ToString() + "/" + playerHealth.maxShield.ToString();
    }
}
