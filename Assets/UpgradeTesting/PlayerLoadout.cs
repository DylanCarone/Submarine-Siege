
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{
    public WeaponDataSO LeftEquippedWeapon { get; private set; }
    public WeaponDataSO RightEquippedWeapon { get; private set; }
    
    public void SetLeftWeapon(WeaponDataSO weapon)
    {
        LeftEquippedWeapon = weapon;
    }
    public void SetRightWeapon(WeaponDataSO weapon)
    {
        RightEquippedWeapon = weapon;
    }
    
}
