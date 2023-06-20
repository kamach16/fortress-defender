using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float damagePerHit;
    public int maxAmmo;
    public float reloadTime;
    public bool fullAuto;
    public float timeBetweenBulletsIfFullAuto;
}
