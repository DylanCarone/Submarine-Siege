using System;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private PlayerStats stats;
    
    private void Start()
    {
        var loadout = PlayerData.Instance.Loadout;

        var upgradeState = loadout.GetLeftWeaponUpgradeData();
        var upgradeStateR = loadout.GetRightWeaponUpgradeData();
        stats = PlayerData.Instance.Stats;
        
        //Debug.Log($"Equipped L: {loadout.LeftEquippedWeapon} | Ammo: {upgradeState.currentValues[WeaponStatType.MaxAmmo]}");
        //Debug.Log($"Equipped R: {loadout.RightEquippedWeapon} | Ammo: {upgradeStateR.currentValues[WeaponStatType.MaxAmmo]}");
        Debug.Log($"Coins: {stats.Coins}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            stats.GainCoins(1);
        }
    }
}
