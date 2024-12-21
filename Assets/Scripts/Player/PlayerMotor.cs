using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public CinemachineVirtualCamera vcam;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float baseFOV = 60f;
    [SerializeField] private float fovChangeSpeed = 5f;
    [SerializeField] private float maxFOVIncrease = 30f;

    private float targetFOV;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        targetFOV = baseFOV;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, targetFOV, fovChangeSpeed * Time.deltaTime);
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * playerSpeed * Time.deltaTime);

        float forwardVelocity = Vector3.Dot(controller.velocity, transform.forward) / 1.5f;
        if(forwardVelocity > maxFOVIncrease)
        {
            forwardVelocity = maxFOVIncrease;
        }

        targetFOV = baseFOV + Mathf.Max(0, forwardVelocity);

        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        controller.Move(playerVelocity * Time.deltaTime);

    }

    public void Jump()
    {
        if (IsGrounded())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }

}
