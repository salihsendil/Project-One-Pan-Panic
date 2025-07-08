using UnityEngine;

public class PlayerInteractionsHandler : MonoBehaviour
{
    [SerializeField] private float maxRayDistance = 1.25f;
    [SerializeField] private float capsuleRadius = 0.15f;
    [SerializeField] private Vector3 rayOffset = new Vector3(0f, 0.5f, 0f);
    private PlayerCarryHandler playerCarryHandler;
    [SerializeField] private IStationInteractable currentInteractableStation;
    [SerializeField] private IStationInteractable previousInteractableStation;

    private RaycastHit hit; //for debug line, delete when job done !!!!

    void Start()
    {
        playerCarryHandler = GetComponent<PlayerCarryHandler>();
        InputHandler.OnInteractionTriggered += TryInteract;
    }

    private void Update()
    {
        InteractionRay();
    }

    private void InteractionRay()
    {
        Vector3 center = transform.position + rayOffset;
        bool isSphereCastHit = Physics.SphereCast(center, capsuleRadius, transform.forward, out hit, maxRayDistance);

        if (isSphereCastHit && hit.collider.gameObject.TryGetComponent<IStationInteractable>(out currentInteractableStation))
        {
            currentInteractableStation.HandleRayHit(true);
        }
        else
        {
            currentInteractableStation = null;
        }

        if (currentInteractableStation != previousInteractableStation)
        {
            previousInteractableStation?.HandleRayHit(false);
            currentInteractableStation?.HandleRayHit(true);
            previousInteractableStation = currentInteractableStation;
        }
    }
    private void TryInteract(bool isAlternate)
    {
        if (currentInteractableStation != null && playerCarryHandler != null)
        {
            if (currentInteractableStation is KitchenStation station)
            {
                playerCarryHandler.HandleStationInteraction(station, isAlternate);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            //Gizmos.DrawWireSphere(transform.position + rayOffset, capsuleRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(hit.point, Vector3.one / 5);
        }
    }
}
