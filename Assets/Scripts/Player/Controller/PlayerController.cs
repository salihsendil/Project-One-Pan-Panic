using UnityEngine;
using Zenject;

// It needs InputHandler.cs. It has integrated gravity, jump, and rotation.
// Gravity depends on whether the character is on the ground or in the air.

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Inject] private InputHandler inputHandler;

    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float speed = 3.2f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;

    [Header("References")]
    [SerializeField] private Vector3 initialSize;

    public Vector3 MovementVector => movementVector;
    public Vector3 InitialSize => initialSize;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        initialSize = transform.localScale;
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleMovement()
    {
        movementVector = ConvertInputToMovementVector(inputHandler.InputVector);
        Vector3 moveDir = rb.position + speed * Time.fixedDeltaTime * movementVector;
        rb.MovePosition(moveDir);
    }

    private void HandleRotation()
    {
        if (movementVector == Vector3.zero) return;

        Quaternion lookAt = Quaternion.LookRotation(movementVector, transform.up);
        Quaternion targetLook = Quaternion.Slerp(rb.rotation, lookAt, Time.fixedDeltaTime * rotationSpeed);

        rb.MoveRotation(targetLook);
    }

    private Vector3 ConvertInputToMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }

    public void EnableGravity(bool isEnabled)
    {
        rb.isKinematic = !isEnabled;
    }
}