using UnityEngine;

[System.Serializable]
public class StatUpgradeData
{
    [Tooltip("Maximum upgrade level for this stat.")]
    public int maxLevel;
    
    [Tooltip("Cost for each upgrade level.")]
    public int[] costPerLevel;
    
    [Tooltip("Value of the stat at each upgrade level.")]
    public float[] valuePerLevel;

}
