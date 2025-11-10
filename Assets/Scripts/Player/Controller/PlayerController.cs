using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;

    //Movement Variables
    private Vector3 movementVector => inputHandler.MovementVector;
    [SerializeField] private float speed = 4f;

    //Rotation Variables
    [SerializeField] private float rotationSpeed = 20f;

    private void FixedUpdate()
    {
        if (movementVector != Vector3.zero)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        transform.position += movementVector * speed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Quaternion targetRot = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
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
