
using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    
    [Header("Transform Data")]
    [SerializeField] private Transform leftGunPosition;
    [SerializeField] private Transform rightGunPosition;
    
    [Header("Weapon Data")]
    [SerializeField] private WeaponBase leftGun;
    [SerializeField] private WeaponBase rightGun;

    private void Start()
    {
        var loadout = PlayerData.Instance.Loadout;
        var leftUpgradeState = PlayerData.Instance.Upgrades.GetUpgradeState(loadout.LeftEquippedWeapon);
        var rightUpgradeState = PlayerData.Instance.Upgrades.GetUpgradeState(loadout.RightEquippedWeapon);
        leftGun.Init(leftUpgradeState);
        rightGun.Init(rightUpgradeState);
    }


    private void Update()
    {
        if (Input.GetKeyDown(playerInput.leftFireButton))
        {
            leftGun.Fire(leftGunPosition);
        }
        if (Input.GetKeyDown(playerInput.rightFireButton))
        {
            rightGun.Fire(rightGunPosition);
        }

        leftGun.Reload(Time.deltaTime);
        rightGun.Reload(Time.deltaTime);
        
    }
}
