using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponUpgradeState
{
    public WeaponDataSO Weapon { get; private set; }

    // Keeps track of the current upgrade levels for each stat
    // this will allow the shop ui to display the current level of each stat
    // and the cost to upgrade each stat
    public Dictionary<WeaponStatType, int> upgradeLevels = new Dictionary<WeaponStatType, int>();

    // Keeps track of the current values of each stat
    // this will allow the weapon to pull the current value of each stat
    // and display it in the ui
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
