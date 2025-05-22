using System;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private PlayerStats stats;

    private void Awake()
    {
        stats = PlayerData.Instance.Stats;
    }

    private void Start()
    {
        var loadout = PlayerData.Instance.Loadout;

        var upgradeState = PlayerData.Instance.Upgrades.GetUpgradeState(loadout.LeftEquippedWeapon);
        
        Debug.Log($"Equipped: {loadout.LeftEquippedWeapon.name} | Ammo: {upgradeState.currentValues[WeaponStatType.MaxAmmo]}");
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
