using System;
using UnityEngine;

public class WeaponUpgradeUI : MonoBehaviour
{
    public WeaponDataSO weaponToUpgrade;
    public WeaponStatType statToUpgrade;

    
    public void UpgradeSelectedWeapon()
    {
        var upgrades = PlayerData.Instance.Upgrades;
        var upgradeState = upgrades.GetUpgradeState(weaponToUpgrade);

        int currentLevel = upgradeState.GetUpgradeLevel(statToUpgrade);
        var upgradeData = weaponToUpgrade.GetUpgradeData(statToUpgrade);

        if (currentLevel >= upgradeData.maxLevel)
        {
            Debug.Log("Stat is already at max level!");
            return;
        }

        int upgradeCost = upgradeData.costPerLevel[currentLevel];

        if (PlayerData.Instance.Stats.Coins < upgradeCost)
        {
            Debug.Log($"Not enough gold. You need {upgradeCost - PlayerData.Instance.Stats.Coins} more");
            return;
        }

        upgrades.UpgradeWeapon(weaponToUpgrade, statToUpgrade);
        PlayerData.Instance.Stats.SpendCoins(upgradeCost);
        

        // Optionally, update UI here to reflect new cost and stat values
    }
}
