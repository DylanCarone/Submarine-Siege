using System;
using UnityEngine;


[Serializable]
public class WeaponSlot
{
    public Transform firePosition;
    public WeaponBase weapon;
    public KeyCode fireButton;

    public void Initialize(WeaponUpgradeState upgradeData)
    {
        if (weapon != null)
        {
            weapon.Init(upgradeData);
        }
    }

    public void UpdateWeapon(float deltaTime)
    {
        if (weapon)
        {
            weapon.Reload(deltaTime);
        }
    }

    public void TryFire()
    {
        if (weapon && firePosition && Input.GetKeyDown(fireButton))
        {
            weapon.Fire(firePosition);
        }
    }
}
