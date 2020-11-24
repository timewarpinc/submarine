using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SubmarineController : MonoBehaviour
{
    private Vector2 inputVector;
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
        Vector3 xDirection = new Vector3(inputVector.x,0 ,0);

        if (xDirection.magnitude > 0 && _rotating)
        {
            targetRotation = Quaternion.LookRotation(xDirection);
        }

        if (inputVector.magnitude > 0 && !NeedsRotating())
        {
            _body.AddForce(inputVector * (Time.deltaTime * playerSpeed), forceMode);
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
        inputVector = input.Get<Vector2>();
    }

    public void OnRotate(InputValue input)
    {
        _rotating = input.isPressed;
    }
}
