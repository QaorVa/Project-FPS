using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject destroyEffect;

    private bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
            if(destroyEffect != null && !isDestroyed)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
            
            isDestroyed = true;
            Destroy(gameObject);
        } else if (other.CompareTag("Wall") || other.CompareTag("Ground"))
        {
            if(destroyEffect != null && !isDestroyed)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
            
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
