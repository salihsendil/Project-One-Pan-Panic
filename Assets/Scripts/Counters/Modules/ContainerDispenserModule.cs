using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class ContainerDispenserModule : MonoBehaviour, IInstantModule
{
    //Zenject
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private SignalBus signalBus;

    //References
    private ItemSocket itemSocket;

    //Data
    [SerializeField] private ContainerItemSO containerItemSO;

    //Stack
    private Stack<ContainerItem> containerStack = new();

    //Transform
    private Vector3 positionOffset = new Vector3(0f, 0.05f, 0f);

    //Respawn Delay
    [SerializeField] private int respawnDelay = 1500;

    private void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
    }

    private void OnEnable()
    {
        signalBus.Subscribe<ContainerItemDespawned>(PrepareReplacement);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<ContainerItemDespawned>(PrepareReplacement);
    }

    private void Start()
    {
        while (TryPlaceContainer()) { }
    }

    private async void PrepareReplacement()
    {
        await Task.Delay(respawnDelay);

        TryPlaceContainer();
    }

    private bool TryPlaceContainer()
    {
        ContainerItem item = poolManager.Spawn<ContainerItem>(containerItemSO.PoolType);
        if (item == null) return false;

        if (!item.TryGetComponent(out IPickable pickable)) return false;

        itemSocket.SetItemToOffset(pickable, positionOffset * containerStack.Count);
        containerStack.Push(item);
        return true;
    }

    public bool TryInteractionInstant(IInteractor _)
    {
        if (containerStack == null || containerStack.Count <= 0) return false;
        containerStack.Pop();

        if (!containerStack.TryPeek(out ContainerItem item)) return false;

        itemSocket.SetItemToOffset(item, positionOffset * (containerStack.Count - 1));
        return true;
    }
}
