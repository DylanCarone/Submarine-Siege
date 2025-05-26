using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [SerializeField] private PlayerStats stats;
    
    public PlayerLoadout Loadout { get; private set; }
    public PlayerUpgradeInventory Upgrades { get; private set; }
    public PlayerStats Stats { get; private set; }

    private void Awake()
    {
        Instance = this;
        Loadout = GetComponent<PlayerLoadout>();
        Upgrades = GetComponent<PlayerUpgradeInventory>();
        Stats = stats;
        stats?.Initialize();
    }



  
}
