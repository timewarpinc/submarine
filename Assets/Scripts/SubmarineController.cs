using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubmarineController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 movVec;
    private Quaternion targetRotation;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float degreesPerSecond = 90f;

    private bool _rotating;
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        targetRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        Vector3 xDirection = new Vector3(movVec.x,0 ,0);
        bool direction = xDirection.magnitude > 0;

        if (direction && _rotating)
        {
            targetRotation = Quaternion.LookRotation(xDirection);
        }

        if (direction && !NeedsRotating())
        {
            controller.Move(movVec * (Time.deltaTime * playerSpeed));
            controller.Move(playerVelocity * Time.deltaTime);
        }

        if (_rotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                targetRotation, degreesPerSecond * Time.deltaTime); ;
        }

    }

    bool NeedsRotating()
    {
        return transform.rotation != targetRotation;
    }

    public void OnMove(InputValue input)
    {
        var inputVec = input.Get<Vector2>();

        movVec = new Vector3(inputVec.x, inputVec.y, 0);
    }

    public void OnRotate(InputValue input)
    {
        _rotating = input.isPressed;
    }
}
