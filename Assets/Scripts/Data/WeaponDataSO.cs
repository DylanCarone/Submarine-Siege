using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data/Weapon", fileName = "SW_New slop")]
public class WeaponDataSO : ScriptableObject
{
    public string weaponName;
    
    public int baseMaxAmmo;
    public StatUpgradeData maxAmmoUpgrade;

    public float baseFireRate;
    public StatUpgradeData fireRateUpgrade;

    public Bullet bulletPrefab;

    public StatUpgradeData GetUpgradeData(WeaponStatType statType)
    {
        switch (statType)
        {
            case WeaponStatType.MaxAmmo: return maxAmmoUpgrade;
            case WeaponStatType.FireRate: return fireRateUpgrade;
            default: return null;
            
        }
    }
    
}
