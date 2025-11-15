using System;
using UnityEngine;
using Zenject;

public class PlayerInteractor : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;

    //Ray Variables
    [SerializeField] private float rayRadius = 0.25f;
    [SerializeField] private float maxRayDistance = 1f;
    [SerializeField] private Vector3 rayOffset = new(0f, 0.75f, 0f);

    private CounterHighlighter currentHighlighter;

    //Events
    public event Action<IInteractable<PlayerCarryingController>> OnCounterInteractionRequest;

    private void OnEnable()
    {
        inputHandler.OnInteractionButtonPressed += HandleInteraction;
    }

    private void OnDisable()
    {
        inputHandler.OnInteractionButtonPressed -= HandleInteraction;
    }

    void Update()
    {
        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        Vector3 rayOrigin = transform.position + rayOffset;
        Vector3 rayDir = transform.forward * maxRayDistance;
        Ray ray = new(rayOrigin, rayDir);
        if (Physics.SphereCast(ray, rayRadius, out RaycastHit hit, maxRayDistance))
        {
            if (hit.collider.gameObject.TryGetComponent(out CounterHighlighter newHighlighter))
            {
                if (currentHighlighter == newHighlighter) { return; }

                if (currentHighlighter != null) { currentHighlighter.HighlightObject(false); }

                currentHighlighter = newHighlighter;
                currentHighlighter.HighlightObject(true);
                return;
            }
        }

        if (currentHighlighter != null)
        {
            currentHighlighter.HighlightObject(false);
            currentHighlighter = null;
        }
    }

    private void HandleInteraction()
    {
        Vector3 rayOrigin = transform.position + rayOffset;
        Vector3 rayDir = transform.forward * maxRayDistance;
        Ray ray = new(rayOrigin, rayDir);
        if (Physics.SphereCast(ray, rayRadius, out RaycastHit hit, maxRayDistance))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable<PlayerCarryingController> interactable ))
            {
                OnCounterInteractionRequest?.Invoke(interactable);
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
