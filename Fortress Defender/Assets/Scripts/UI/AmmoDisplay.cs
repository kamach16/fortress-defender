using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] public Slider ammoSlider;

    public void UpdateAmmoDisplay(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();

        ammoSlider.maxValue = maxAmmo;
        ammoSlider.value = currentAmmo;
    }
}
