using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }
}
