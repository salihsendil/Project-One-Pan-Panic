using UnityEngine;
using Unity;
using UnityEditor;

public class PlayerInteractionsHandler : MonoBehaviour
{
    [SerializeField] private float maxRayDistance = 1.25f;
    [SerializeField] private float capsuleRadius = 0.15f;
    [SerializeField] private Vector3 rayOffset = new Vector3(0f, 0.5f, 0f);
    private PlayerCarryHandler playerCarryHandler;

    private RaycastHit hit; //just for draw gizmos, delete when job done!!

    void Start()
    {
        playerCarryHandler = GetComponent<PlayerCarryHandler>();
        InputHandler.OnInteractionsKeyPressed += TryInteract;
    }

    private void TryInteract()
    {
        Vector3 center = transform.position + rayOffset;
        bool isSphereCastHit = Physics.SphereCast(center, capsuleRadius, transform.forward, out hit, maxRayDistance);
        if (isSphereCastHit)
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(playerCarryHandler);
            }

            //Debug.Log($"i hit: {hit.point}");
            //Debug.Log($"i hit: {hit.collider.name}");
            //Debug.Log($"distance from player: {Vector3.Distance(transform.position, hit.point)}");
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
