using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    public float movementSpeed = 12f; 
    public float jumpHeight = 2f; //Mäts i meter

    [Space]

    public float _jumpBuffer = 0.2f;
    float jumpBuffer = 0;

    bool jumpQueued = false;

    [Space]
    public float gravity = -20f;

    [Space]
    public Transform groundcheckTransform;
    public float groundcheckRadius;
    bool isGrounded;

    Vector3 velocity;

    CharacterController characterController;

    private void Awake(){
        characterController = GetComponent<CharacterController>();

        PlayerInputEventManager input = FindObjectOfType<PlayerInputEventManager>();

        input.jumpKey += OnSpacebar;
    }
    void Update(){
        HandleMovement();
        HandleJumping();

        HandleGravity();

        HandleTimers();

        isGrounded = Physics.CheckSphere(groundcheckTransform.position, groundcheckRadius, Layers.ground | Layers.structure);
        if (isGrounded && velocity.y < 0) velocity.y = -5f;
    }
    void HandleMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * xInput + transform.forward * zInput;

        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);
    }
    void HandleJumping()
    {
        if (jumpQueued && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpQueued = false;
        }
    }
    void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    void HandleTimers()
    {
        jumpBuffer -= Time.deltaTime;
        jumpQueued = jumpBuffer <= 0 ? false : jumpQueued;
    }
    void OnSpacebar()
    {
        jumpBuffer = _jumpBuffer;
        jumpQueued = true;
    }
}
