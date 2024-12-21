using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour, IDamageable
{
    EnemyHealth enemyHealth;

    private void Start()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    public void TakeDamage(int damage)
    {
        enemyHealth.TakeDamage(damage * 2);
    }
}
