using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player's inventory of weapon upgrades, including tracking the current upgrade states
/// for different weapons and providing methods for retrieving and upgrading these states.
/// </summary>
public class PlayerUpgradeInventory : MonoBehaviour
{
    
    // Keep track of the current upgrades of weapons 
    /// <summary>
    /// Represents the list of current weapon upgrades available in the player's inventory.
    /// Each entry in the list tracks the current upgrade state for a specific weapon,
    /// including its upgrade levels and current stat values.
    /// </summary>
    
    List<WeaponUpgradeState> currentWeaponUpgrades = new List<WeaponUpgradeState>();

    /// Retrieves the upgrade state of a specified weapon. If the weapon does not have
    /// an existing upgrade state, a new one is created and added to the inventory.
    /// <param name="weaponDataSo">The weapon data representing the weapon whose
    /// upgrade state is to be retrieved or created.</param>
    /// <return>Returns the WeaponUpgradeState associated with the specified weapon
    /// data.</return>
    public WeaponUpgradeState GetUpgradeState(WeaponDataSO weaponDataSo)
    {

        var state = currentWeaponUpgrades.Find(weapon => weapon.Weapon == weaponDataSo);
        if (state == null)
        {
            state = new WeaponUpgradeState(weaponDataSo);
            currentWeaponUpgrades.Add(state);
        }

        return state;
    }

    /// <summary>
    /// Upgrades a specific stat of a given weapon.
    /// </summary>
    /// <param name="weapon">The weapon whose stat will be upgraded.</param>
    /// <param name="stat">The type of stat to upgrade (e.g., MaxAmmo, FireRate).</param>
    public void UpgradeWeapon(WeaponDataSO weapon, WeaponStatType stat)
    {
        var upgradeState = GetUpgradeState(weapon);
        upgradeState.UpgradeStat(stat);
    }  
    
    
}
