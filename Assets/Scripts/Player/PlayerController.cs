using UnityEngine;
using Zenject;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [Inject] private InputHandler inputHandler;
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float acceleration = 350f;
    [SerializeField] private float maxSpeed = 4.5f;
    [SerializeField] private bool hasBusy = false;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool showVelocityDebug;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Getter - Setter")]
    public bool HasBusy { get => hasBusy; set => hasBusy = value; }
    public bool IsMoving { get => isMoving; }
    private Vector3 movementVector => inputHandler.MovementVector;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!hasBusy && IsMovementValueValid())
        {
            PlayerMove();
            PlayerRotation();
        }

        else
        {
            isMoving = false;
        }
    }

    private bool IsMovementValueValid()
    {
        return movementVector != Vector3.zero;
    }

    private void PlayerMove()
    {
        Vector3 newVelocity = movementVector * Time.fixedDeltaTime * acceleration;

        if (newVelocity.magnitude > maxSpeed)
        {
            newVelocity = newVelocity.normalized * maxSpeed;
        }

        rb.linearVelocity = newVelocity;

        if (showVelocityDebug)
        {
            Debug.Log($"h�z: {rb.linearVelocity}");
        }

        isMoving = true;
    }

    private void PlayerRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(transform.position, rb.linearVelocity.magnitude * movementVector + transform.position);
        }
    }
}