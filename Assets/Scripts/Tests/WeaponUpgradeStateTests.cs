// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
//
// public class WeaponUpgradeStateTests
// {
//     [Test]
//     public void UpgradeStat_IncreasesLevel()
//     {
//         var maxAmmoUpgrade = new StatUpgradeData
//         {
//             maxLevel = 3,
//             costPerLevel = new int[] { 100, 200, 300 },
//             valuePerLevel = new float[] { 12, 14, 16 }
//         };
//
//         var weaponData = ScriptableObject.CreateInstance<WeaponDataSO>();
//         weaponData.baseMaxAmmo = 10;
//         weaponData.maxAmmoUpgrade = maxAmmoUpgrade;
//
//         var upgradeState = new WeaponUpgradeState(weaponData);
//         
//         
//         // --- Act ---
//         upgradeState.UpgradeStat(WeaponStatType.MaxAmmo);
//         
//         // --- assert ---
//         Assert.AreEqual(1, upgradeState.GetUpgradeLevel(WeaponStatType.MaxAmmo));
//         Assert.AreEqual(12, upgradeState.currentValues[WeaponStatType.MaxAmmo]);
//
//     }
// }
