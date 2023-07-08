using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldDisplay : MonoBehaviour
{
    [SerializeField] private Slider shieldSlider;

    public void UpdateShield(float currentShield)
    {
        shieldSlider.value = currentShield;
    }
}
