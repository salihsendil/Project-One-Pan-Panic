using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
public class PlayerCarryingController : MonoBehaviour
{
    private ItemSocket itemSocket;

    private void Awake()
    {
        TryGetComponent(out itemSocket);
    }

    private void OnEnable() //debug
    {
        SetItem(GetItem());
    }

    public bool HasItem() => itemSocket.HasItem();

    public BaseKitchenItem GetItem() => itemSocket.GetItem();

    public void SetItem(BaseKitchenItem obj) => itemSocket.SetItem(obj);

    public BaseKitchenItem RemoveItem() => itemSocket.RemoveItem();
}
