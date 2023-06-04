using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void UpdateHealth(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}
