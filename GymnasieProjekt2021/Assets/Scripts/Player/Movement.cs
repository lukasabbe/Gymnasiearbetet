using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    public float movementSpeed = 12f; 
    public float jumpHeight = 2f; //Mäts i meter

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
    }
    void Update(){
        isGrounded = Physics.CheckSphere(groundcheckTransform.position, groundcheckRadius, Layers.ground | Layers.structure);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * xInput + transform.forward * zInput;

        characterController.Move(moveDirection * movementSpeed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        HandleGravity();
    }
    void HandleGravity(){
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
