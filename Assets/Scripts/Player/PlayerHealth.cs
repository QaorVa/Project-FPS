using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private PlayerUI playerUI;
    private PlayerShoot playerShoot;
    private InputManager inputManager;

    private void Start()
    {
        currentHealth = maxHealth;
        playerUI = GetComponent<PlayerUI>();
        playerShoot = GetComponent<PlayerShoot>();
        inputManager = GetComponent<InputManager>();
    }

    public void TakeDamage(int damage)
    {
        CameraShakeTrigger.Instance.TriggerShake(new Vector3(.1f, 0, .1f));
        currentHealth -= damage;
        playerUI.UpdateHealthCount(currentHealth);
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerUI.ShowDeathUI();
        playerShoot.enabled = false;
        inputManager.enabled = false;
        Time.timeScale = 0;
    }
}
