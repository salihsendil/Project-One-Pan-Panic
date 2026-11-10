using UnityEngine;

public class TEST_Script_Class : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rotationSpeed = 90f;

    private void FixedUpdate()
    {
        rb.MoveRotation(
            rb.rotation * Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f)
        );
    }
}
