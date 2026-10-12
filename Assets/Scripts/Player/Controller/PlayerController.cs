using UnityEngine;

// It needs InputHandler.cs. It has integrated gravity, jump, and rotation.
// Gravity depends on whether the character is on the ground or in the air.

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float verticalVelocity;
    [SerializeField] private float speed = 3.2f;
    [SerializeField] private float gravityForce = 9.81f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;


    public Vector3 MovementVector => movementVector;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
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
                verticalVelocity = -2f;
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

    private Vector3 ConvertInputToMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y).normalized;
    }
}