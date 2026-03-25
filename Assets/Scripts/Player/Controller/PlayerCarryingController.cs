using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
public class PlayerCarryingController : MonoBehaviour, IInteractor
{
    private ItemSocket itemSocket;

    private void Awake()
    {
        TryGetComponent(out itemSocket);
    }

    public bool HasItem => itemSocket.HasItem;

    public IPickable GetItem => itemSocket.GetItem;

    public void SetItem(IPickable obj) => itemSocket.SetItem(obj);

    public IPickable RemoveItem() => itemSocket.RemoveItem();
}
