using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class PlayerInteractionController : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;

    //Ray Variables
    [SerializeField] private float rayRadius = 0.15f;
    [SerializeField] private float maxRayDistance = 0.5f;

    //Raycast Settings
    [SerializeField] private LayerMask layerMask;
    private readonly RaycastHit[] raycastHits = new RaycastHit[1];

    //Interactable
    private IInteractable currentInteractable;

    //References
    private IInteractor interactor;
    private ItemSocket itemSocket;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
        itemSocket = GetComponent<ItemSocket>();
    }

    private void Start()
    {
        inputHandler.OnInteractionStarted += InteractionStarted;
        inputHandler.OnInteractionPerformed += InteractionPerformed;
        inputHandler.OnInteractionCanceled += InteractionCanceled;
    }

    private void OnDisable()
    {
        inputHandler.OnInteractionStarted -= InteractionStarted;
        inputHandler.OnInteractionPerformed -= InteractionPerformed;
        inputHandler.OnInteractionCanceled -= InteractionCanceled;
    }

    private void Update()
    {
        if (TryGetInteractable(out IInteractable interactable))
        {
            interactable.HighlightInteractable(true);

            if (currentInteractable != interactable)
            {
                currentInteractable?.HighlightInteractable(false);
                InteractionCanceled();
                currentInteractable = interactable;
            }
        }

        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.HighlightInteractable(false);
                InteractionCanceled();
            }
        }
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

    private void InteractionStarted()
    {
        currentInteractable?.InteractionStarted(interactor);
    }

    private void InteractionPerformed()
    {
        currentInteractable?.InteractionPerformed(interactor);
    }

    private void InteractionCanceled()
    {
        currentInteractable?.InteractionCanceled(interactor);
        currentInteractable = null;
    }
}
