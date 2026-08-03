using UnityEngine;
using Zenject;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;

    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float speed = 3.6f;
    [SerializeField] private float groundGravity = 1f;
    [SerializeField] private float gravityForce = 9.81f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 12f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        movementVector = inputHandler.MovementVector;

        HandleRotation();
        ApplyGravity();
        HandleMovement();
    }

    private void HandleMovement()
    {
        movementVector *= speed;
        movementVector.y = verticalVelocity;

        characterController.Move(movementVector * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -groundGravity;
        }

        else
        {
            verticalVelocity -= gravityForce * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {
        if (movementVector == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(movementVector, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
    }
}


//physics based movement if needed
//private void FixedUpdate()
//{
//    if (movementVector != Vector3.zero)
//    {
//        HandleMovement();
//        HandleRotation();
//    }

//    else
//    {
//        SlowDownVelocity();
//    }

//}

//private void SlowDownVelocity()
//{
//    if (rb.linearVelocity.magnitude > (Vector3.one * Time.deltaTime).magnitude)
//    {
//        rb.linearVelocity -= rb.linearVelocity * 0.25f;
//    }
//}

//private void HandleMovement()
//{
//    float acceleration = 20f;
//    Vector3 movementForce = inputHandler.MovementVector.normalized * acceleration;
//    rb.AddForce(movementForce, ForceMode.Acceleration);
//    if (rb.linearVelocity.magnitude > speed)
//    {
//        rb.linearVelocity = rb.linearVelocity.normalized * speed;
//    }
//}
