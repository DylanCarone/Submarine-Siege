
using System;
using UnityEngine;

/// <summary>
/// Manages the player's weapon systems and handles weapon firing logic.
/// This class controls both left and right weapons, their initialization from player loadout,
/// and processing inputs for firing.
/// </summary>

public class PlayerAttack : MonoBehaviour
{
    /// <summary>
    /// Reference to the player input component for handling button presses.
    /// </summary>
    [SerializeField] private PlayerInput playerInput;

    [Header("Weapons")] 

    [Tooltip("The left weapon slot containing weapon, fire position, and input key.")]
    [SerializeField] private WeaponSlot leftWeapon = new WeaponSlot();
    
    [Tooltip("The right weapon slot containing weapon, fire position, and input key.")]
    [SerializeField] private WeaponSlot rightWeapon = new WeaponSlot();

    [Tooltip("Flag to determine whether to use an externally injected loadout instead of the default one.")]
    [SerializeField] private bool useInjectedLoadout = false;
    
    // The player's current weapon loadout configuration
    private PlayerLoadout loadout;

    // Event triggered when a weapon is fired, providing the weapon instance and its transform
    public event Action<WeaponBase, Transform> OnWeaponFired;
    
    private void Start()
    {
        SetLoadout(PlayerData.Instance.Loadout);
    }

    
    /// <summary>
    /// Sets a new loadout for the player
    /// Initialized weapons based on the new loadout if it's valid
    /// </summary>
    /// <param name="playerLoadout"></param>
    public void SetLoadout(PlayerLoadout playerLoadout)
    {
        loadout = playerLoadout;
        if (loadout)
        {
            InitializeWeapons();
        }
    }

    
    private void InitializeWeapons()
    {
        try
        {
            PlayerLoadout currentLoadout = loadout ?? PlayerData.Instance.Loadout;

            if (currentLoadout)
            {
                var leftUpgradeData = currentLoadout.GetLeftWeaponUpgradeData();
                var rightUpgradeData = currentLoadout.GetRightWeaponUpgradeData();

                leftWeapon.Initialize(leftUpgradeData);
                rightWeapon.Initialize(rightUpgradeData);
            }
            else
            {
                Debug.LogWarning("No loadout set.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to initialize weapons: {e.Message}");
        }
    }
    

    private void Update()
    {
        leftWeapon.TryFire();
        rightWeapon.TryFire();
        
        leftWeapon.UpdateWeapon(Time.deltaTime);
        rightWeapon.UpdateWeapon(Time.deltaTime);

    }
}
