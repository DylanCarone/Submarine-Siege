using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private int maxAmmo;
    [SerializeField] private float recoverySpeed;
    
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed = 4f;
    [SerializeField] private int bulletDamage = 1;


    private int currentAmmo;
    private float recoveryTimer;

    public int CurrentAmmo => currentAmmo;


    public event Action<int> OnAmmoChanged;

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

}
