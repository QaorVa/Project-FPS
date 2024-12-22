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

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        currentHoverHeight = hoverHeight + Random.Range(-hoverHeightOffset, hoverHeightOffset);
        sidewaysSpeed = Random.Range(sidewaysSpeed - 1, sidewaysSpeed + 1);
    }

    // Update is called once per frame
    void Update()
    {
        Hover();
        MoveToTarget();
    }

    void Hover()
    {
        

        Ray ray = new Ray(transform.position, -transform.up);
        int layerMask = LayerMask.GetMask("Ground");

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
        {
            float desiredYPosition = hitInfo.point.y + currentHoverHeight;

            Vector3 currentPosition = transform.position;
            currentPosition.y = Mathf.Lerp(currentPosition.y, desiredYPosition, Time.deltaTime);

            transform.position = currentPosition;
        }
    }

    void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
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
            transform.Translate(sidewaysDirection * sidewaysSpeed * Time.deltaTime, Space.World);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMovingSideways = false;
    }
}
