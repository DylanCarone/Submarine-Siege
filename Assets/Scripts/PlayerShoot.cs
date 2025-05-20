using UnityEngine;
using UnityEngine.UI;

public partial class PlayerShoot : MonoBehaviour
{
    [SerializeField] private KeyCode leftFire;
    [SerializeField] private KeyCode rightFire;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;

    [SerializeField] private Image[] leftBulletUI;
    [SerializeField] private Image[] rightBulletUI;

    [SerializeField] private float ammoRecoverySpeed = 2.5f;
    
    private int leftBullets = 5;
    private int rightBullets = 5;

    private int maxBullets = 5;

    private float leftRecoveryTimer = 0f;
    private float rightRecoveryTimer = 0f;
    

    // Update is called once per frame
    void Update()
    {
        UpdateBulletUI();
        if (Input.GetKeyDown(leftFire) && leftBullets > 0)
        {
            leftBullets--;
            ShootBullet(leftPosition);
        }   
        if (Input.GetKeyDown(rightFire) && rightBullets> 0)
        {
            rightBullets--;
            ShootBullet(rightPosition);
        }

        (leftBullets, leftRecoveryTimer) = RefreshBullet(leftBullets, leftRecoveryTimer);
        (rightBullets, rightRecoveryTimer) = RefreshBullet(rightBullets, rightRecoveryTimer);
    }

    void ShootBullet(Transform firePosition)
    {
        var newBullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
    }

    private (int, float) RefreshBullet(int bulletType,float bulletTimer)
    {
        if (bulletType < maxBullets)
        {
            bulletTimer += Time.deltaTime;
            if (bulletTimer >= ammoRecoverySpeed)
            {
                bulletType++;
                bulletTimer = 0f;
            }
        }
        else
        {
            bulletTimer = 0f;
        }

        return (bulletType, bulletTimer);
    }

    void UpdateBulletUI()
    {
        for (int i = 0; i < leftBulletUI.Length; i++)
        {
            leftBulletUI[i].enabled = i < leftBullets;
        }
        
        for (int i = 0; i < rightBulletUI.Length; i++)
        {
            rightBulletUI[i].enabled = i < rightBullets;
        }
    }
    
}
