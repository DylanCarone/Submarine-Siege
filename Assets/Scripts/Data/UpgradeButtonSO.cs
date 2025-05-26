using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade" , menuName = "Upgrades/Upgrade")]
public class UpgradeButtonSO : ScriptableObject
{
    [Header("Upgrade information")]
    public string upgradeName;
    [TextArea(2, 5)] public string description;

    [Header("Upgrade Type")] 
    public WeaponStatType statType;

    [Header("Upgrade Details")] public WeaponDataSO targetWeapon;
    public StatUpgradeData upgradeData;
    
    [Header("Upgrade Value")]
    public float upgradeAmount = 1.0f;

    
    [Header("UI Elements")]
    public Sprite upgradeIcon;




    public virtual bool ApplyUpgrade(PlayerUpgradeInventory playerUpgradeInventory)
    {
        if (!playerUpgradeInventory)
        {
            return false;
        }
        
        playerUpgradeInventory.UpgradeWeapon(targetWeapon, statType);
        return true;
    }
    


}
