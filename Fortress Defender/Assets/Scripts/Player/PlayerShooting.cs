using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private float damagePerHit;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool fullAuto;
    [SerializeField] private int currentAmmo;
    [SerializeField] private bool isReloading;
    [SerializeField] private float timeBetweenBulletsIfFullAuto;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject groundHitSplatVFX;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AmmoDisplay ammoDisplay;

    private Coroutine shootingCoroutine;

    private void Start()
    {
        SelectWeapon(currentWeapon); // delete this when shop will be in building
    }

    private void Update()
    {
        Shooting();
        Reloading();
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log("Selected " + currentWeapon);

        damagePerHit = currentWeapon.damagePerHit;
        maxAmmo = currentWeapon.maxAmmo;
        reloadTime = currentWeapon.reloadTime;
        fullAuto = currentWeapon.fullAuto;
        timeBetweenBulletsIfFullAuto = currentWeapon.timeBetweenBulletsIfFullAuto;

        currentAmmo = maxAmmo;
        ammoDisplay.UpdateAmmoDisplay(currentAmmo, maxAmmo);
    }

    private void Shooting()
    {
        if (gameManager.lost || isReloading) return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (fullAuto) StartFullAutoShooting();
            else ShootOneBullet();
        }
        else if (!Input.GetButton("Fire1")) StopShooting();        
    }

    private void StartFullAutoShooting()
    {
        shootingCoroutine = StartCoroutine(FullAutoShooting());
    }

    private void StopShooting()
    {
        StopAllCoroutines(); // it only stopping "FullAutoShooting" coroutine
    }

    private IEnumerator FullAutoShooting()
    {
        while (true)
        {
            ShootOneBullet();
            yield return new WaitForSeconds(timeBetweenBulletsIfFullAuto);
        }
    }

    private void ShootOneBullet()
    {
        if (currentAmmo <= 0) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        currentAmmo--;
        ammoDisplay.UpdateAmmoDisplay(currentAmmo, maxAmmo);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("One bullet fired");

            EnemyController enemy = hit.transform.gameObject.GetComponent<EnemyController>();

            if (enemy == null)
            {
                CreateGroundHitSplatVFX(hit.point);
            }
            else
            {
                enemy.TakeDamage(damagePerHit, hit.point);
                //enemy.ShowEnemyHitSplat(hit.point);
            }
        }
    }

    private void Reloading()
    {
        if (Input.GetButtonDown("Fire2") && !isReloading)
        {
            StopShooting();

            ammoDisplay.ammoSlider.value = 0;
            ammoDisplay.ammoSlider.maxValue = 1;

            isReloading = true;
        }

        if (isReloading) ReloadAnimation();
    }

    public void ReloadAnimation()
    {
        Debug.Log("Reloading...");
        ammoDisplay.ammoSlider.value += Time.deltaTime / reloadTime;

        if(ammoDisplay.ammoSlider.value >= ammoDisplay.ammoSlider.maxValue)
        {
            isReloading = false;
            ammoDisplay.ammoSlider.maxValue = maxAmmo;
            ammoDisplay.ammoSlider.value = maxAmmo;
            currentAmmo = maxAmmo;
            ammoDisplay.UpdateAmmoDisplay(currentAmmo, maxAmmo);
        }
    }

    private void CreateGroundHitSplatVFX(Vector3 spawnPos)
    {
        GameObject vfx = Instantiate(groundHitSplatVFX, spawnPos, Quaternion.identity);
        Destroy(vfx, 1f);
    }
}
