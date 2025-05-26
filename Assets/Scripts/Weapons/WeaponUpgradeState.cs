using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the upgrade state of a weapon, including its current stat levels and values.
/// This class is used to handle weapon statistics, upgrade levels, and recalculations based on upgrades.
/// </summary>
[System.Serializable]
public class WeaponUpgradeState
{
    /// <summary>
    /// Reference to the associated weapon data scriptable object.
    /// Provides access to the static configuration data for the weapon.
    /// Read-only
    /// </summary>
    public WeaponDataSO Weapon { get; private set; }
    
    // Private dictionary tracking the current upgrade level for each weapon stat type. 
    private Dictionary<WeaponStatType, int> upgradeLevels = new Dictionary<WeaponStatType, int>();

    // Public dictionary mapping each stat type to its current calculated value
    public Dictionary<WeaponStatType, float> currentValues = new Dictionary<WeaponStatType, float>();


    /// <summary>
    /// Initialized a new weapon upgrade state with the specified weapon data.
    /// Sets all upgrade levels to 0 and calculates initial stat values.
    /// </summary>
    /// <param name="weaponDataSo">The weapon data scriptable object to associate with this upgrade state</param> 
    public WeaponUpgradeState(WeaponDataSO weaponDataSo)
    {
        Weapon = weaponDataSo;
        foreach (WeaponStatType stat  in System.Enum.GetValues(typeof(WeaponStatType)))
        {
            upgradeLevels[stat] = 0;
        }
        Recalculate();
    }


   // Returns the base value for the specified stat type from the associated weapon data.
    private float GetBaseStatValue(WeaponStatType stat)
    {
        switch (stat)
        {
            case WeaponStatType.MaxAmmo: return Weapon.baseMaxAmmo;
            case WeaponStatType.FireRate: return Weapon.baseFireRate;
            // Add more as needed
            default: return 0f;
        }
    }


    // Recalculates the current stat values based on the current upgrade levels.
    private void Recalculate()
    {
        Debug.Log($"{Weapon}");
        foreach (var stat in upgradeLevels.Keys)
        {
            int level = upgradeLevels[stat];
            StatUpgradeData upgradeData = Weapon.GetUpgradeData(stat);
            if (upgradeData == null)
            {
                Debug.LogError($"Upgrade data was not found from {Weapon}");
            }

            float value = GetBaseStatValue(stat);

            if (upgradeData != null && upgradeData.valuePerLevel != null && level > 0)
            {
                int valueIndex = level - 1;
                if (valueIndex < upgradeData.valuePerLevel.Length)
                {
                    value = upgradeData.valuePerLevel[valueIndex];
                    
                }
                else
                {
                    value = upgradeData.valuePerLevel[upgradeData.valuePerLevel.Length - 1]; 
                    Debug.LogWarning($"Upgrade level {level} exceeds configured values for {stat} on {Weapon.weaponName}");
                }
            }

            currentValues[stat] = value;
        }
    }

    /// <summary>
    /// Gets the current upgrade level for a specified stat type.
    /// </summary>
    /// <param name="stat">The stat type to retrieve the upgrade level for</param>
    /// <returns>The current upgrade level of the specified stat, or 0 if the stat isn't in the dictionary</returns>
    public int GetUpgradeLevel(WeaponStatType stat)
    {
        return upgradeLevels.ContainsKey(stat) ? upgradeLevels[stat] : 0;
    }


    /// <summary>
    /// Increased the upgrade level for the specified stat type by 1.
    /// </summary>
    /// <param name="stat">The stat type to upgrade</param>
    public void UpgradeStat(WeaponStatType stat)
    {
        if (upgradeLevels.ContainsKey(stat))
        {
            upgradeLevels[stat]++;
        }
        else
        {
            upgradeLevels[stat] = 1;
        }
        Recalculate();
    }

}
