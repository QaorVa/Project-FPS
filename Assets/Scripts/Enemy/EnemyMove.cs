using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float hoverHeight;
    [SerializeField] private float hoverHeightOffset;
    [SerializeField] private float sidewaysSpeed = 3f;
    [SerializeField] private float minSidewaysTime = 1f;
    [SerializeField] private float maxSidewaysTime = 3f;

    private float currentHoverHeight;
    private bool isMovingSideways = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        currentHoverHeight = hoverHeight + Random.Range(-hoverHeightOffset, hoverHeightOffset);
        sidewaysSpeed = Random.Range(sidewaysSpeed - 1, sidewaysSpeed + 1);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Hover();
        MoveToTarget();
    }

    void Hover()
    {
        Ray ray = new Ray(new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), -transform.up);
        int layerMask = LayerMask.GetMask("Ground");

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
        {
            float desiredYPosition = hitInfo.point.y + currentHoverHeight;

            Vector3 targetPosition = rb.position;
            targetPosition.y = Mathf.Lerp(targetPosition.y, desiredYPosition, Time.fixedDeltaTime);

            rb.MovePosition(targetPosition);
        }
    }

    void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            Vector3 direction = (target.position - rb.position).normalized;
            Vector3 movement = direction * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movement);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isMovingSideways)
        {
            Debug.Log("Enemy detected, moving sideways");
            StartCoroutine(MoveSideways());
        }
    }

    private IEnumerator MoveSideways()
    {
        isMovingSideways = true;
        float sidewaysTime = Random.Range(minSidewaysTime, maxSidewaysTime);
        float elapsedTime = 0f;

        Vector3 sidewaysDirection = Vector3.right;
        if (Random.value > 0.5f) sidewaysDirection = Vector3.left;

        while (elapsedTime < sidewaysTime)
        {
            Vector3 movement = sidewaysDirection * sidewaysSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMovingSideways = false;
    }
}
