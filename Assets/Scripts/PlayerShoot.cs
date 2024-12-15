using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerUI playerUI;
    private InputManager inputManager;
    private Camera cam;

    [SerializeField] private float distance = 100f;
    [SerializeField] private int damage = 50;
    [SerializeField] private GameObject bulletHolePrefab;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
        playerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.onFoot.Shoot.triggered)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, distance))
        {
            if(hitInfo.collider.CompareTag("Ground"))
            {
                Debug.Log("Hit the ground");
                SpawnBulletHole(hitInfo);
            } else if (hitInfo.collider.GetComponent<IDamageable>() != null)
            {
                IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
                damageable.TakeDamage(damage);
                playerUI.OnEnemyHit();
            }
        }
    }

    private void SpawnBulletHole(RaycastHit hitInfo)
    {
        GameObject bulletHole = Instantiate(bulletHolePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        bulletHole.transform.SetParent(hitInfo.collider.transform);
        Destroy(bulletHole, 5f);
    }
}
