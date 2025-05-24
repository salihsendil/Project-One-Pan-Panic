using UnityEngine;
using Unity;
using UnityEditor;
using System;

public class PlayerInteractionsHandler : MonoBehaviour
{
    [SerializeField] private float maxRayDistance = 1.25f;
    [SerializeField] private float capsuleRadius = 0.15f;
    [SerializeField] private Vector3 rayOffset = new Vector3(0f, 0.5f, 0f);
    private PlayerCarryHandler playerCarryHandler;
    private RaycastHit hit; //for debug line, delete when job done !!!!
    private IInteractable currentInteractable;
    private IInteractable previousInteractable;

    void Start()
    {
        playerCarryHandler = GetComponent<PlayerCarryHandler>();
        InputHandler.OnInteractionsKeyPressed += TryInteract;
    }

    private void Update()
    {
        InteractionRay();
    }

    private void InteractionRay()
    {
        Vector3 center = transform.position + rayOffset;
        bool isSphereCastHit = Physics.SphereCast(center, capsuleRadius, transform.forward, out hit, maxRayDistance);

        if (isSphereCastHit && hit.collider.gameObject.TryGetComponent<IInteractable>(out currentInteractable))
        {
            currentInteractable.HandleRayHit(true);
        }
        else
        {
            currentInteractable = null;
        }

        if (currentInteractable != previousInteractable)
        {
            previousInteractable?.HandleRayHit(false);
            currentInteractable?.HandleRayHit(true);
            previousInteractable = currentInteractable;
        }
    }
    private void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(playerCarryHandler);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(transform.position + rayOffset, capsuleRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(hit.point, Vector3.one / 5);
        }
    }
}
