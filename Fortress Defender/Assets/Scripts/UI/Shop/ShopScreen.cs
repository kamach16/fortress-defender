using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private GameObject defenceObjectsContainer;
    [SerializeField] private GameObject defenceObjectsPreShow;
    [SerializeField] public Transform towerPlaceSpot;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealth playerHealth;

    private void OnEnable()
    {
        UpdateTexts();
        HideDefenceObjectsContainer();
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

    public void SelectTowerSpot(Transform selectedTowerSpot)
    {
        defenceObjectsPreShow.SetActive(false);
        defenceObjectsContainer.SetActive(true);

        towerPlaceSpot = selectedTowerSpot;
    }

    private void HideDefenceObjectsContainer()
    {
        defenceObjectsPreShow.SetActive(true);
        defenceObjectsContainer.SetActive(false);
    }
}
