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
    [SerializeField] private float maxSpeed = 5.5f;
    [SerializeField] private bool canMove;
    [SerializeField] private bool showVelocityDebug;
    public bool CanMove { get => canMove; }

    private Vector3 movementVector => inputHandler.MovementVector;


    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 15f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (IsMovementValueValid())
        {
            PlayerMove();
            PlayerRotation();
        }
    }

    private bool IsMovementValueValid()
    {
        canMove = movementVector != Vector3.zero;
        return canMove;
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
            Debug.Log($"hýz: {rb.linearVelocity}");
        }
        //Vector3 force = movementVector * Time.fixedDeltaTime * speed + transform.position;
        //rb.MovePosition(force);
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
            Gizmos.DrawLine(transform.position, rb.linearVelocity.magnitude * movementVector + transform.position);
        }
    }
}