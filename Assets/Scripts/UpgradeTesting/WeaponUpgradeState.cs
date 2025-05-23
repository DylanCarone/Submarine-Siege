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
    /// </summary>
    public WeaponDataSO Weapon { get; private set; }


    // Dictionary mapping each stat type to its current upgrade level
    private Dictionary<WeaponStatType, int> upgradeLevels = new Dictionary<WeaponStatType, int>();

    // Dictionary mapping each stat type to its current calculated value
    public Dictionary<WeaponStatType, float> currentValues = new Dictionary<WeaponStatType, float>();


    public WeaponUpgradeState(WeaponDataSO weaponDataSo)
    {
        Weapon = weaponDataSo;
        foreach (WeaponStatType stat  in System.Enum.GetValues(typeof(WeaponStatType)))
        {
            upgradeLevels[stat] = 0;
        }
        Recalculate();
    }

    public float GetBaseStatValue(WeaponStatType stat)
    {
        switch (stat)
        {
            case WeaponStatType.MaxAmmo: return Weapon.baseMaxAmmo;
            case WeaponStatType.FireRate: return Weapon.baseFireRate;
            // Add more as needed
            default: return 0f;
        }
    }


    public void Recalculate()
    {
        Debug.Log($"{Weapon}");
        foreach (var stat in upgradeLevels.Keys)
        {
            int level = upgradeLevels[stat];
            StatUpgradeData upgradeData = Weapon.GetUpgradeData(stat);

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

    public int GetUpgradeLevel(WeaponStatType stat)
    {
        return upgradeLevels.ContainsKey(stat) ? upgradeLevels[stat] : 0;
    }


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
