using System;
using TMPro;
using UnityEngine;

public class WeaponUpgradeUI : MonoBehaviour
{
    
    [SerializeField] private WeaponUpgradeUISelection weaponUISelection;
    [SerializeField] WeaponStatType statToUpgrade;

    [SerializeField] private TextMeshProUGUI costText;
    
    WeaponDataSO weapon;

    private PlayerUpgradeInventory upgrades;
    private WeaponUpgradeState upgradeState;
    private int currentLevel;
    private StatUpgradeData upgradeData;
    private int upgradeCost;
    
    void Awake()
    {
        weapon = weaponUISelection.WeaponDataSo;
    }

    private void Start()
    {
        GetUpgradeData();

        if (currentLevel < upgradeData.maxLevel)
        {
            upgradeCost = upgradeData.costPerLevel[currentLevel];
            costText.text = $"Cost: {upgradeCost}";
            
        }
        else
        {
            costText.text = "";
        }

    }

    private void GetUpgradeData()
    {
        upgrades = PlayerData.Instance.Upgrades;
        upgradeState = upgrades.GetUpgradeState(weapon);

        currentLevel = upgradeState.GetUpgradeLevel(statToUpgrade);
        upgradeData = weapon.GetUpgradeData(statToUpgrade);
        
    }

    public void UpgradeSelectedWeapon()
    {
        GetUpgradeData();
        if (currentLevel >= upgradeData.maxLevel)
        {
            Debug.Log("Stat is already at max level!");
            costText.text = "";
            return;
        }

        if (PlayerData.Instance.Stats.Coins < upgradeCost)
        {
            Debug.Log($"Not enough gold. You need {upgradeCost - PlayerData.Instance.Stats.Coins} more");
            return;
        }

        upgrades.UpgradeWeapon(weapon, statToUpgrade);
        PlayerData.Instance.Stats.SpendCoins(upgradeCost);
        
        upgradeCost = upgradeData.costPerLevel[currentLevel];
        costText.text = $"Cost: {upgradeCost}";
        

        // Optionally, update UI here to reflect new cost and stat values
    }
    
    
}
