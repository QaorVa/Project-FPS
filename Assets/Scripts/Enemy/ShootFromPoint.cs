using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFromPoint : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    private float shotTimer;
    [SerializeField] private float startShotTimer = 1.5f;

    public EnemyRange enemyRange;

    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyRange.isPlayerInRange)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (shotTimer <= 0)
        {
            audioSource.Play();
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
             shotTimer = startShotTimer;
         }
         else
         {
             shotTimer -= Time.deltaTime;
         }
    }
}
