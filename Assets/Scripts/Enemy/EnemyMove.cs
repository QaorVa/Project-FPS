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

    private float currentHoverHeight;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Hover();
        MoveToTarget();
    }

    void Hover()
    {
        currentHoverHeight = hoverHeight + Random.Range(-hoverHeightOffset, hoverHeightOffset);

        Ray ray = new Ray(transform.position, -transform.up);
        int layerMask = LayerMask.GetMask("Ground");

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
        {
            float desiredYPosition = hitInfo.point.y + currentHoverHeight;

            Vector3 currentPosition = transform.position;
            currentPosition.y = Mathf.Lerp(currentPosition.y, desiredYPosition, Time.deltaTime * moveSpeed);

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
}
