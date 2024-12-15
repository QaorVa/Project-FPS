using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public CinemachineVirtualCamera vcam;
    public GameObject weapon;
    public float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = transform.localRotation;
        
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        targetRotation = Quaternion.Euler(xRotation, 0f, 0f);

        vcam.transform.localRotation = targetRotation;
        weapon.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
