using UnityEngine;
using Zenject;

// It needs InputHandler.cs. It has integrated gravity, jump, and rotation.
// Gravity depends on whether the character is on the ground or in the air.

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Inject] private InputHandler inputHandler;

    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float speed = 3.2f;
    [SerializeField] private float groundGravityForce = 2f;
    [SerializeField] private float airGravityForce = 9.81f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;

    [Header("References")]
    [SerializeField] private Vector3 initialSize;

    public Vector3 MovementVector => movementVector;
    public Vector3 InitialSize => initialSize;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        initialSize = transform.localScale;
    }

    private void Update()
    {
        movementVector = ConvertInputToMovementVector(inputHandler.InputVector);

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
                verticalVelocity = -groundGravityForce;
        }

        else
        {
            verticalVelocity -= airGravityForce * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {
        if (movementVector == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(movementVector, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
    }

    private Vector3 ConvertInputToMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }

    public void EnableGravity(bool isEnabled)
    {
        airGravityForce = isEnabled ? 9.81f : 0;
        groundGravityForce = isEnabled ? 2f : 0;
        verticalVelocity = 0f;
    }
}