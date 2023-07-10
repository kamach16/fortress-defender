using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private GameObject defenceObjectsContainer;
    [SerializeField] private GameObject defenceObjectsPreShow;
    [SerializeField] public Transform towerPlaceSpot;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemySpawner enemySpawner;

    private Image selectedTowerSpotImage;

    private void OnEnable()
    {
        UpdateTexts();
        HideDefenceObjectsContainer();
        SetVariables();
    }

    private void SetVariables()
    {
        if (selectedTowerSpotImage != null) selectedTowerSpotImage.color = new Color32(255, 255, 255, 255);
        waveNumberText.text = "WAVE " + (enemySpawner.waveNumber + 1); // + 1 because waveNumber is changing when player click continue button in shop 
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
        if (selectedTowerSpotImage != null) selectedTowerSpotImage.color = new Color32(255, 255, 255, 255);

        defenceObjectsPreShow.SetActive(false);
        defenceObjectsContainer.SetActive(true);

        towerPlaceSpot = selectedTowerSpot;
    }

    public void ChangeTowerSpotImageColor(Image image)
    {
        selectedTowerSpotImage = image;

        image.color = new Color32(0, 185, 255, 255);
    }

    private void HideDefenceObjectsContainer()
    {
        defenceObjectsPreShow.SetActive(true);
        defenceObjectsContainer.SetActive(false);
    }
}
