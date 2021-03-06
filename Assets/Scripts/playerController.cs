﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

	public Transform camTransform => Camera.main.transform;

    private float currentSpeed = 0.0f;
    private float speedSmoothVelocity = 0.0f;
    private float speedSmoothTime = 0.1f;
    public float rotationSpeed = 0.1f;
    private float gravity = 3.0f;

	private CharacterController m_controller;
    public CharacterController controller { get { if (!m_controller) { m_controller = GetComponent<CharacterController>(); } return m_controller; } }

    public bool isInputEnabled = true;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputEnabled)
        {
            PlayerMovement();
        }
    }

    void PlayerMovement()
    {
        //stores if and what wasd is pressed
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //stores the forward vector of the cameras transform
        Vector3 forward = camTransform.forward;
        //stores the right vector of the cameras transform
        Vector3 right = camTransform.right;

        forward.Normalize();
        right.Normalize();

        Debug.Log(movementInput.x + " " + movementInput.y);

        Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;
        Vector3 gravityVector = Vector3.zero;
       
        if (!controller.isGrounded)
        {
            gravityVector.y -= gravity;
        }

        Vector3 desiredForwards = Vector3.ProjectOnPlane(desiredMoveDirection, Vector3.up);
        if (desiredForwards == Vector3.zero)
        {
            desiredForwards = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        }
        if (desiredForwards != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredForwards), rotationSpeed);
            //transform.rotation = Quaternion.LookRotation(desiredMoveDirection);
        }

        float targetSpeed = moveSpeed * movementInput.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        controller.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);

        controller.Move(gravityVector * Time.deltaTime);
    }
}
