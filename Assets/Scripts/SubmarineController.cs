using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubmarineController : MonoBehaviour
{
    private Vector3 playerVelocity;
    private Vector3 movVec;
    private Quaternion targetRotation;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float degreesPerSecond = 90f;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;

    private bool _rotating;

    private Rigidbody _body;

    private void Start()
    {
        targetRotation = transform.rotation;
        _body = GetComponent<Rigidbody>();
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
            _body.AddForce(movVec * (Time.deltaTime * playerSpeed), forceMode);
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
