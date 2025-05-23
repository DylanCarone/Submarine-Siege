using UnityEngine;

public class WeaponUpgradeUISelection : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponToUpgrade;

    public WeaponDataSO WeaponDataSo => weaponToUpgrade;
}
