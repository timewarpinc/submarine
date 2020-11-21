using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubmarineController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed = 2.0f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    public void OnMove(InputValue input)
    {
        var inputVec = input.Get<Vector2>();

        var moveVec = new Vector3(inputVec.x, inputVec.y, 0);
        
        controller.Move(moveVec * (Time.deltaTime * playerSpeed));

        if (moveVec != Vector3.zero)
        {
            gameObject.transform.forward = moveVec;
        }
    }
}
