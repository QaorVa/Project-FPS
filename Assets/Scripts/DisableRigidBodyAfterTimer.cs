using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRigidBodyAfterTimer : MonoBehaviour
{
    [SerializeField] private float timer = 2f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DisableRigidBody());
    }

    IEnumerator DisableRigidBody()
    {
        yield return new WaitForSeconds(timer);
        rb.freezeRotation = true;
        rb.isKinematic = true;  
    }
}
