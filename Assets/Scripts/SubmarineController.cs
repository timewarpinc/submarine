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
    [SerializeField] private float playerSpeed = 2.0f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        controller.Move(movVec * (Time.deltaTime * playerSpeed));

        if (playerVelocity != Vector3.zero)
        {
            gameObject.transform.forward = movVec;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue input)
    {
        var inputVec = input.Get<Vector2>();

        movVec = new Vector3(inputVec.x, inputVec.y, 0);
    }
}
