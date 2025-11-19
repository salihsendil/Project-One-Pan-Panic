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

    public KitchenItem GetItem() => itemSocket.GetItem();

    public void SetItem(KitchenItem obj) => itemSocket.SetItem(obj);

    public KitchenItem RemoveItem() => itemSocket.RemoveItem();
}
