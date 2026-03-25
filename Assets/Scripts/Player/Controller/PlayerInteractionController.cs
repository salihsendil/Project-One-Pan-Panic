using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerCarryingController))]
public class PlayerInteractionController : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;

    //Ray Variables
    [SerializeField] private float rayRadius = 0.25f;
    [SerializeField] private float maxRayDistance = 1f;

    //Raycast Settings
    [SerializeField] private LayerMask layerMask;
    private readonly RaycastHit[] raycastHits = new RaycastHit[1];

    //Interactable
    private IInteractable currentInteractable;

    //References
    private IInteractor interactor;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
    }

    private void Start()
    {
        inputHandler.OnInteractionStarted += TestInteractionStarted;
        inputHandler.OnInteractionPerformed += TestInteractionPerformed;
        inputHandler.OnInteractionCanceled += TestInteractionCanceled;
    }

    private void OnDisable()
    {
        inputHandler.OnInteractionStarted -= TestInteractionStarted;
        inputHandler.OnInteractionPerformed -= TestInteractionPerformed;
        inputHandler.OnInteractionCanceled -= TestInteractionCanceled;
    }

    private void Update()
    {
        //optimization required
        if (TryGetInteractable(out currentInteractable)) { }
    }

    private bool TryGetInteractable(out IInteractable interactable)
    {
        interactable = null;

        Vector3 rayOrigin = transform.position;
        Vector3 rayDir = transform.forward.normalized;
        Ray ray = new(rayOrigin, rayDir);

        int hitCount = Physics.SphereCastNonAlloc(ray, rayRadius, raycastHits, maxRayDistance, layerMask, QueryTriggerInteraction.Ignore);

        if (hitCount > 0)
        {
            RaycastHit hit = raycastHits[0];

            if (hit.collider.TryGetComponent(out interactable))
            {
                return true;
            }
        }
        return false;
    }

    private void TestInteractionStarted()
    {
        currentInteractable?.InteractionStarted(interactor);
    }

    private void TestInteractionPerformed()
    {
        currentInteractable?.InteractionPerformed(interactor);
    }

    private void TestInteractionCanceled()
    {
        currentInteractable?.InteractionCanceled(interactor);
        currentInteractable = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDir = transform.forward * maxRayDistance;
        Ray ray = new(rayOrigin, rayDir);
        Gizmos.DrawRay(ray);
    }
}
