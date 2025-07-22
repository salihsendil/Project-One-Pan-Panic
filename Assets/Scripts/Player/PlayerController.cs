using UnityEngine;
using Zenject;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [Inject] private InputHandler inputHandler;

    [Header("Movement")]
    [SerializeField] private float playerSize = 0.5f;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private bool hasBusy = false;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool showVelocityDebug;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 20f;

    [Header("Getter - Setter")]
    public bool HasBusy { get => hasBusy; set => hasBusy = value; }
    public bool IsMoving { get => isMoving; }
    private Vector3 movementVector => inputHandler.MovementVector;

    void FixedUpdate()
    {
        if (!hasBusy && IsMovementValueValid())
        {
            PlayerRotation();

            if (CanMove())
            {
                isMoving = true;
                PlayerMove();
            }
        }

        else
        {
            isMoving = false;
        }
    }

    private bool CanMove()
    {
        return !Physics.Raycast(transform.position, movementVector, playerSize);
    }

    private bool IsMovementValueValid()
    {
        return movementVector != Vector3.zero;
    }

    private void PlayerMove()
    {
        transform.position += movementVector * speed * Time.deltaTime;
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
            Ray ray = new Ray(transform.position, transform.position + movementVector * playerSize);
            Gizmos.DrawRay(ray);
            //Gizmos.DrawLine(transform.position, rb.linearVelocity.magnitude * movementVector + transform.position);
        }
    }
}