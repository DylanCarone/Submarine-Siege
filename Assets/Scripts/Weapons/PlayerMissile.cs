using UnityEngine;

public class PlayerMissile : WeaponBase
{
    [SerializeField] private Bullet bulletPrefab;
    
     public override void Fire(Transform firePosition)
     {
         Debug.Log(currentAmmo);
         if (currentAmmo <= 0)
         {
             return;
         }
         Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
         currentAmmo--;
     }
     
 }
