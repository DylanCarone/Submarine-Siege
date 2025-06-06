using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private float recoverySpeed;
    [SerializeField] private float bulletSpeed = 4f;
    [SerializeField] private int bulletDamage = 1;

    protected Bullet bulletPrefab;
    protected int maxAmmo;

    protected int currentAmmo;
    private float recoveryTimer;

    public int CurrentAmmo => currentAmmo;


    public event Action<int> OnAmmoChanged;


    public virtual void Init(WeaponUpgradeState weaponData)
    {
        maxAmmo = (int)weaponData.currentValues[WeaponStatType.MaxAmmo];
        bulletPrefab = weaponData.Weapon.bulletPrefab;
        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo);
    }

    public void Fire(Transform firePosition)
    {
        if (currentAmmo <= 0)
        {
            return;
        }
        var bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
        bullet.Initialize(bulletSpeed, bulletDamage);
        currentAmmo--;
        OnAmmoChanged?.Invoke(currentAmmo);
    }



    private void Awake()
    {
        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo);
    }

    public void Reload(float deltaTime)
    {
        if (currentAmmo < maxAmmo)
        {
            recoveryTimer += deltaTime;
            if (recoveryTimer >= recoverySpeed)
            {
                currentAmmo++;
                OnAmmoChanged?.Invoke(currentAmmo);
                recoveryTimer = 0f;
            }
        }
        else
        {
            recoveryTimer = 0f;
        }
    }
    
    public void ApplyUpgrade(WeaponStatType stat, int value)
    {
        switch (stat)
        {
            case WeaponStatType.MaxAmmo:
                maxAmmo += value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stat), stat, null);
        }
    }

}
