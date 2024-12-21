using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float angleOffset = 0f;
    [SerializeField] private bool rotateOnY;
    [SerializeField] private bool rotateOnZ;

    [SerializeField] private GameObject pivotPoint;

    [HideInInspector] public bool isPlayerInRange;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotateToTarget();

    }

    private void RotateToTarget()
    {
        if (player != null)
        {
            Vector3 direction = player.position - pivotPoint.transform.position;

            direction.Normalize();

            float angleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + angleOffset;
            float angleZ = Mathf.Atan2(direction.y, Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z)) * Mathf.Rad2Deg;

            if(!rotateOnY) angleY = 0;
            if(!rotateOnZ) angleZ = 0;

            Quaternion targetRotation = Quaternion.Euler(0, angleY, angleZ);

            pivotPoint.transform.rotation = Quaternion.Slerp(pivotPoint.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
