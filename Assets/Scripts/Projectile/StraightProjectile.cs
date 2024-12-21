using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 10f;
    public Vector3 direction;


    public float acceleration = 2f;

    void Update()
    {
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
        else
        {
            speed += acceleration * Time.deltaTime;
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }
}