using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour, ITransferItemHandler
{
    private PlayerController playerController;

    [Header("Hold Point")]
    [SerializeField] private Transform holdPointTransform;

    [Header("Currently Holding")]
    [SerializeField] private KitchenItem currentKitchenItem;

    public bool HasKitchenItem => currentKitchenItem != null;

    public KitchenItem GetKitchenItem => currentKitchenItem;

    public bool HasBusyForProcess { get => playerController.HasBusy; set => playerController.HasBusy = value; }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void GiveKitchenItem(out KitchenItem kitchenItem)
    {
        kitchenItem = currentKitchenItem;
        currentKitchenItem = null;
    }

    public void ReceiveKitchenItem(KitchenItem kitchenItem)
    {
        if (kitchenItem == null) { return; }

        currentKitchenItem = kitchenItem;
        currentKitchenItem.transform.SetParent(holdPointTransform);
        currentKitchenItem.transform.position = holdPointTransform.position;
    }

    public void HandleStationInteraction(KitchenStation station, bool isAlternate)
    {
        if (station == null) { return; }

        station.SetTransferItemHandler(this);
        if (isAlternate)
        {
            station.InteractAlternate();
        }
        else
        {
            station.Interact();
        }
    }
}
