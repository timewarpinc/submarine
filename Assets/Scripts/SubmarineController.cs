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

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        if (LookAtDirection(movVec))
        {
            controller.Move(movVec * (Time.deltaTime * playerSpeed));

            controller.Move(playerVelocity * Time.deltaTime);
        }

    }

    bool LookAtDirection(Vector3 moveDirection)
    {
        Vector3 xDirection = new Vector3(moveDirection.x,0 ,0);

        if (xDirection.magnitude > 0)
        {
            targetRotation = Quaternion.LookRotation(xDirection);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                targetRotation, degreesPerSecond * Time.deltaTime);

            return targetRotation == transform.rotation;
        }
        else
        {
            return true;
        }

    }

    public void OnMove(InputValue input)
    {
        var inputVec = input.Get<Vector2>();

        movVec = new Vector3(inputVec.x, inputVec.y, 0);
    }
}
