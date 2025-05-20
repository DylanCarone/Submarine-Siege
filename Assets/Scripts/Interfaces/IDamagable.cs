using UnityEngine;

public interface IDamagable
{
    public void DestroyTarget(bool diedByPlayer);
    public void TakeDamage(int damage);
}
