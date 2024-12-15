using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecoilTrigger : MonoBehaviour
{
    public static CameraRecoilTrigger Instance { get; private set; }

    public CinemachineImpulseSource impulseSource;

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
    public void TriggerShake()
    {
        impulseSource.GenerateImpulse();
    }
}
