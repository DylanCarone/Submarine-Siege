using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeInventory : MonoBehaviour
{
    
    // Keep track of the current upgrades of weapons 
    [HideInInspector]
    public List<WeaponUpgradeState> currentWeaponUpgrades = new List<WeaponUpgradeState>();

    public WeaponUpgradeState GetUpgradeState(WeaponDataSO weaponDataSo)
    {

        var state = currentWeaponUpgrades.Find(weapon => weapon.Weapon == weaponDataSo);
        if (state == null)
        {
            Debug.Log("Not found creating new");
            state = new WeaponUpgradeState(weaponDataSo);
            currentWeaponUpgrades.Add(state);
        }

        return state;
    }

    public void UpgradeWeapon(WeaponDataSO weapon, WeaponStatType stat)
    {
        var upgradeState = GetUpgradeState(weapon);
        upgradeState.UpgradeStat(stat);
    }
    
    
    
    
}
