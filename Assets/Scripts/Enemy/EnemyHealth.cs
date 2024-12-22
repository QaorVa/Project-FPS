using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private Vector3 explosionShakeCameraDir;
    [SerializeField] private bool shakeCamera = true;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        
        currentHealth -= damage;
        Debug.Log("Remaining Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.enemiesKilledCount++;
        Debug.Log("Enemies Killed: " + GameManager.enemiesKilledCount);
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        if (shakeCamera)
        {
            CameraShakeTrigger.Instance.TriggerShake(explosionShakeCameraDir);
        }
        Destroy(gameObject);
    }
}
