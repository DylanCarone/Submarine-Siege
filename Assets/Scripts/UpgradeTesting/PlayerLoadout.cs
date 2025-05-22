
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{

    [SerializeField] private WeaponDataSO startingLeftWeapon;
    [SerializeField] private WeaponDataSO startingRightWeapon;
    
    public WeaponDataSO LeftEquippedWeapon { get; private set; }
    public WeaponDataSO RightEquippedWeapon { get; private set; }

    private void Awake()
    {
        SetLeftWeapon(startingLeftWeapon);
        SetRightWeapon(startingRightWeapon);
    }

    public void SetLeftWeapon(WeaponDataSO weaponData)
    {
        if (weaponData == null)
        {
            LeftEquippedWeapon = null;
            return;
        }

        LeftEquippedWeapon = weaponData;
    }
    public void SetRightWeapon(WeaponDataSO weaponData)
    {
        if (weaponData == null)
        {
            RightEquippedWeapon = null;
            return;
        }

        RightEquippedWeapon = weaponData;
    }

    public WeaponUpgradeState GetLeftWeaponUpgradeData()
    {
        return PlayerData.Instance.Upgrades.GetUpgradeState(LeftEquippedWeapon);
    }
    
    public WeaponUpgradeState GetRightWeaponUpgradeData()
    {
        return PlayerData.Instance.Upgrades.GetUpgradeState(RightEquippedWeapon);
    }

}
