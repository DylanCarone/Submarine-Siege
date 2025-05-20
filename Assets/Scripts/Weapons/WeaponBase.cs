using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private int maxAmmo;
    [SerializeField] private float recoverySpeed;
    protected int currentAmmo;


    private float recoveryTimer;
    public abstract void Fire(Transform firePosition);

    
    public void Reload(float deltaTime)
    {
        if (currentAmmo < maxAmmo)
        {
            recoveryTimer += deltaTime;
            if (recoveryTimer >= recoverySpeed)
            {
                currentAmmo++;
                recoveryTimer = 0f;
            }
        }
        else
        {
            recoveryTimer = 0f;
        }
    }

}
