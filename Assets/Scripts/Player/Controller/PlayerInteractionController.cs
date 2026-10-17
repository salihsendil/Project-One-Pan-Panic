using UnityEngine;

// This class requires InputHandler.cs and ItemSocket.cs (which implements IInteractor).
// It controls player interactions by listening to interaction events from InputHandler.
//
// In Update, it tries to find an interactable counter every frame.
// The found counter is cached to keep track of the current interactable counter and highlight it.
//
// If a counter is found and there is no current counter, it is set as the current counter
// and highlighted.
//
// If a different counter is found, the current counter's hold interaction is cancelled,
// its highlight is disabled, and the new counter is set as the current counter and highlighted.
//
// If no counter is found, the current counter's highlight is disabled and the current counter is set to null.

[RequireComponent(typeof(ItemSocket))]
public class PlayerInteractionController : MonoBehaviour
{
    [Header("References")]
    private IInteractor currentInteractor;
    private IInteractableCounter currentInteractable;
    [SerializeField] private InputHandler inputHandler;

    [Header("Ray Config")]
    [SerializeField] private float rayLength = 1.5f;
    [SerializeField] private float rayRadius = 0.3f;


    private void Awake()
    {
        currentInteractor = GetComponent<IInteractor>();
    }

    private void OnEnable()
    {
        inputHandler.OnInteractInstantPerformed += InteractInstant;
        inputHandler.OnInteractHoldStarted += InteractHoldStart;
        inputHandler.OnInteractHoldCanceled += InteractHoldCanceled;
    }

    private void OnDisable()
    {
        inputHandler.OnInteractInstantPerformed -= InteractInstant;
        inputHandler.OnInteractHoldStarted -= InteractHoldStart;
        inputHandler.OnInteractHoldCanceled -= InteractHoldCanceled;
    }

    private void Update()
    {
        GetInteractable();
    }

    private void GetInteractable()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.SphereCast(ray, rayRadius, out RaycastHit hit, rayLength))
        {
            if (hit.collider.TryGetComponent(out IInteractableCounter interactable))
            {
                interactable.HighlightInteractable(true);

                if (currentInteractable == null)
                {
                    currentInteractable = interactable;
                    return;
                }

                if (currentInteractable != interactable)
                {
                    InteractHoldCanceled();
                    currentInteractable.HighlightInteractable(false);
                    currentInteractable = null;
                    currentInteractable = interactable;
                    return;
                }
            }
        }

        else
        {
            if (currentInteractable != null)
            {
                InteractHoldCanceled();
                currentInteractable.HighlightInteractable(false);
                currentInteractable = null;
            }
        }

    }

    private void InteractInstant()
    {
        currentInteractable?.InteractInstant(currentInteractor);
    }

    private void InteractHoldStart()
    {
        currentInteractable?.InteractHoldStarted(currentInteractor);
    }

    private void InteractHoldCanceled()
    {
        currentInteractable?.InteractHoldCanceled(currentInteractor);
    }

    //private void OnDrawGizmos()
    //{
    //    //Ray ray = new Ray(transform.position, transform.forward);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, rayRadius);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position + (transform.forward * rayLength), rayRadius);
    //    Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
    //}
}