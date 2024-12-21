using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerUI playerUI;
    private InputManager inputManager;
    private Camera cam;

    [Header("Shoot Stats")]
    [SerializeField] private float distance = 100f;
    [SerializeField] private int damage = 50;
    [SerializeField] private int maxAmmo = 6;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate = 0.3f;

    [Header("Effects")]
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Vector3 maxRecoil;
    [SerializeField] private Vector3 minRecoil;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSourceShoot;
    [SerializeField] private AudioSource audioSourceReload;


    private int currentAmmo;
    private bool isReloading;
    private float fireRateTimer;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
        playerUI = GetComponent<PlayerUI>();

        currentAmmo = maxAmmo;
        fireRateTimer = 0f;


        playerUI.UpdateAmmoCount(currentAmmo, maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.onFoot.Shoot.triggered && fireRateTimer <= 0)
        {
            Shoot();
        }

        if(inputManager.onFoot.Reload.triggered && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }

        if(fireRateTimer > 0)
        {
               fireRateTimer -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if(isReloading)
        {
            return;
        }

        if(currentAmmo <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        
        audioSourceShoot.Play();
        muzzleFlash.Play();
        gunAnimator.Play("Shoot");
        currentAmmo--;
        fireRateTimer = fireRate;
        playerUI.UpdateAmmoCount(currentAmmo, maxAmmo);
        Debug.Log("Ammo: " + currentAmmo + " / " + maxAmmo);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        int layerMask = ~LayerMask.GetMask("Teleport");

        CameraRecoilTrigger.Instance.TriggerShake(new Vector3(Random.Range(minRecoil.x,maxRecoil.x), Random.Range(minRecoil.y, maxRecoil.y), Random.Range(minRecoil.z, maxRecoil.z)));
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, distance, layerMask))
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
            } else if(hitInfo.collider.CompareTag("Teleportable Object"))
            {
                Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = hitInfo.collider.transform.position - transform.position;

                    forceDirection.Normalize();

                    float forceMagnitude = damage / 3f;
                    rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
                }
            }
        }
    }

    private void SpawnBulletHole(RaycastHit hitInfo)
    {
        GameObject bulletHole = Instantiate(bulletHolePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        bulletHole.transform.SetParent(hitInfo.collider.transform);
        Destroy(bulletHole, 5f);
    }

    private IEnumerator Reload()
    {
        audioSourceReload.Play();
        gunAnimator.Play("Reload");
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        Debug.Log("Reloaded!");
        isReloading = false;
        playerUI.UpdateAmmoCount(currentAmmo, maxAmmo);
    }

}
