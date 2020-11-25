using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidStream : MonoBehaviour
{


    [SerializeField] private float force = 90f;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;
    [SerializeField] private Vector3 direction = Vector3.up + Vector3.right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ApplyForce(Collider otherCollider)
    {

        var otherBody = otherCollider.attachedRigidbody;
        if (!otherBody.CompareTag("Player"))
        {
            return;
        }


        otherBody.AddForce(direction * (Time.deltaTime * force), forceMode);
    }

    private void OnTriggerEnter(Collider other)
    {
        ApplyForce(other);
    }

    private void OnTriggerStay(Collider other)
    {
        ApplyForce(other);
    }
}
