
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{

    [SerializeField] private WeaponDataSO startingLeftWeapon;
    [SerializeField] private WeaponDataSO startingRightWeapon;
    
    public WeaponDataSO LeftEquippedWeapon { get; private set; }
    public WeaponDataSO RightEquippedWeapon { get; private set; }

    private void Awake()
    {
        SetLeftWeapon(startingLeftWeapon);
        SetRightWeapon(startingRightWeapon);
        
    }

    public void SetLeftWeapon(WeaponDataSO weapon)
    {
        LeftEquippedWeapon = weapon;
    }
    public void SetRightWeapon(WeaponDataSO weapon)
    {
        RightEquippedWeapon = weapon;
    }
    
}
