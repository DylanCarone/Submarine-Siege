using System;
using UnityEngine;

public class DestoryOther : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamagable>()?.DestroyTarget(false);
        Destroy(other.gameObject);
    }
    
}
