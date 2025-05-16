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
    [SerializeField] private float speed = 4f;
    private Vector3 movementVector => inputHandler.MovementVector;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 25f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerMove();
        PlayerRotation();
    }

    private void PlayerMove()
    {
        Vector3 force = movementVector * Time.fixedDeltaTime * speed + transform.position;
        rb.MovePosition(force);
    }

    private void PlayerRotation()
    {
        if (movementVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Debug.DrawLine(transform.position, speed * movementVector + transform.position, Color.red);
        }
    }
}