using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Weapon firstWeapon;
    [SerializeField] private float damagePerHit;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool fullAuto;
    [SerializeField] private int currentAmmo;
    [SerializeField] private bool isReloading;
    [SerializeField] private float timeBetweenBulletsIfFullAuto;
    [SerializeField] private bool isActive;
    [SerializeField] private bool canReload = true;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip reloadSFX;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject groundHitSplatVFX;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AmmoDisplay ammoDisplay;
    [SerializeField] private AudioSource audioSource;

    private Coroutine shootingCoroutine;

    private void Start()
    {
        SelectWeapon(firstWeapon); // first weapon
    }

    private void OnEnable()
    {
        GameManager.OnLevelWin += DeactiveWeapon;
        GameManager.OnNewLevelStarted += ActiveWeapon;
        GameManager.OnDefeat += DeactiveWeapon;
        GameManager.OnDefeat += DontAllowReload;
        GameManager.OnPause += DeactiveWeapon;
        GameManager.OnPause += DontAllowReload;
        GameManager.OnUnpause += ActiveWeapon;
        GameManager.OnUnpause += AllowReload;
    }

    private void OnDisable()
    {
        GameManager.OnLevelWin -= DeactiveWeapon;
        GameManager.OnNewLevelStarted -= ActiveWeapon;
        GameManager.OnDefeat -= DeactiveWeapon;
        GameManager.OnDefeat -= DontAllowReload;
        GameManager.OnPause -= DeactiveWeapon;
        GameManager.OnPause -= DontAllowReload;
        GameManager.OnUnpause -= ActiveWeapon;
        GameManager.OnUnpause -= AllowReload;
    }

    private void Update()
    {
        Shooting();
        Reloading();
    }

    private void DeactiveWeapon()
    {
        isActive = false;
        StopShooting();
    }

    private void ActiveWeapon()
    {
        isActive = true;
    }

    private void DontAllowReload()
    {
        canReload = false;
    }

    private void AllowReload()
    {
        canReload = true;
    }

    public void SelectWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        damagePerHit = currentWeapon.damagePerHit;
        maxAmmo = currentWeapon.maxAmmo;
        reloadTime = currentWeapon.reloadTime;
        fullAuto = currentWeapon.fullAuto;
        timeBetweenBulletsIfFullAuto = currentWeapon.timeBetweenBulletsIfFullAuto;
        shootSFX = currentWeapon.shootSFX;
        reloadSFX = currentWeapon.reloadSFX;

        currentAmmo = maxAmmo;
        ammoDisplay.UpdateAmmoDisplay(currentAmmo, maxAmmo);
    }

    private void Shooting()
    {
        if (gameManager.lost || isReloading || !isActive) return;

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

        audioSource.PlayOneShot(shootSFX);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("One bullet fired");

            EnemyController enemy = hit.transform.root.gameObject.GetComponent<EnemyController>();

            if (enemy == null)
            {
                CreateGroundHitSplatVFX(hit.point);
            }
            else
            {
                enemy.TakeDamage(damagePerHit, hit.point);
                enemy.ShowEnemyHitSplat(hit.point);
            }
        }
    }

    private void Reloading()
    {
        if (!canReload) return;

        if (Input.GetButtonDown("Fire2") && !isReloading)
        {
            StopShooting();

            audioSource.PlayOneShot(reloadSFX);

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
