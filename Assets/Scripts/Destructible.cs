using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private float respawnDuration = 5f;
    private Vector3 objectOriginPosition;
    private Quaternion objectOriginRotation;

    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;
    private Rigidbody rb;

    private AudioSource audioSource;


    private void Start()
    {
        objectOriginPosition = transform.position;
        objectOriginRotation = transform.rotation;

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        /*if (PlayerHealth.isDead)
        {
            transform.position = objectOriginPosition;
            transform.rotation = objectOriginRotation;
        }*/
    }

    public IEnumerator RespawnObject()
    {
        yield return new WaitForSeconds(respawnDuration);
        transform.position = objectOriginPosition;
        transform.rotation = objectOriginRotation;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    public void DisableObject()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        audioSource.Play();

        StartCoroutine(RespawnObject());
    }
}
