using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private KillRecoveryService recoveryService;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerController player)) return;
        Debug.Log("düþtü");
        recoveryService.Recover(player);
    }
}
