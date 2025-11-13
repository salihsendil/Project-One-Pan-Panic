using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    //Ray Variables
    [SerializeField] private float rayRadius = 0.25f;
    [SerializeField] private float maxDistance = 1f;
    [SerializeField] private Vector3 rayOffset = new(0f, 0.75f, 0f);

    //Events
    public event Action<BaseCounter> OnInteractionRequest;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractionRay();
        }
    }

    private void InteractionRay()
    {
        Vector3 rayOrigin = transform.position + rayOffset;
        Vector3 rayDir = transform.forward * maxDistance;
        Ray ray = new(rayOrigin, rayDir);
        if (Physics.SphereCast(ray, rayRadius, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.gameObject.TryGetComponent(out BaseCounter counter))
            {
                OnInteractionRequest?.Invoke(counter);
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Vector3 rayOrigin = transform.position + rayOffset;
        //Vector3 rayDir = transform.forward * maxDistance;
        //Debug.DrawRay(rayOrigin, rayDir, Color.red);
        //Gizmos.DrawWireSphere(rayOrigin, rayRadius);
        //Gizmos.DrawWireSphere(rayDir + rayOrigin, rayRadius);
    }

}
