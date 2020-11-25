using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SubmarineController : MonoBehaviour
{
    private Vector2 inputVector;
    private Quaternion targetRotation;
    [SerializeField] private float forwardSpeed = 2.0f;
    [SerializeField] private float reverseSpeed = 0.5f;
    [SerializeField] private float ballastSpeed = 0.5f;
    [SerializeField] private float degreesPerSecond = 90f;
    [SerializeField] private ForceMode forwardEngineForceMode = ForceMode.Force;
    [SerializeField] private ForceMode reverseEngineForceMode = ForceMode.Force;
    [SerializeField] private ForceMode ballastForceMode = ForceMode.Force;


    private bool _rotating;

    private Rigidbody _body;

    private void Start()
    {
        targetRotation = transform.rotation;
        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var xDirection = new Vector3(inputVector.x,0 ,0);

        var lookRotation = targetRotation;
        if (xDirection.magnitude > 0)
        {
            lookRotation = Quaternion.LookRotation(xDirection);
            if (_rotating)
            {
                targetRotation = lookRotation;
            }
        }

        if (inputVector.magnitude > 0 && !NeedsRotating())
        {
            var speed = (lookRotation == targetRotation) ? forwardSpeed : reverseSpeed;
            if (lookRotation == targetRotation) // facing forward
            {
                _body.AddForce(xDirection * (Time.deltaTime * forwardSpeed), forwardEngineForceMode);
            }
            else // reverse power
            {
                _body.AddForce(xDirection * (Time.deltaTime * reverseSpeed), reverseEngineForceMode);
            }

            _body.AddForce(new Vector3(0, inputVector.y, 0) * (Time.deltaTime * ballastSpeed), ballastForceMode);
        }

        if (_rotating || (lookRotation != transform.rotation && xDirection.magnitude > 0))
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
