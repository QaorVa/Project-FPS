using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    public static CameraShakeTrigger Instance { get; private set; }

    public CinemachineImpulseSource impulseSource;
    public float shakeIntensity = 1f;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to trigger the camera shake
    public void TriggerShake(Vector3 dir)
    {
        impulseSource.GenerateImpulseWithVelocity(dir * shakeIntensity);
    }
}
