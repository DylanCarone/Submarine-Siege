using System;
using UnityEngine;

public class DestoryOther : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<EnemySub>()?.DestoryEnemyByTime();
        Destroy(other.gameObject);
    }
    
}
