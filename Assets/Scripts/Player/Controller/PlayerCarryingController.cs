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

    public GameObject GetItem() => itemSocket.GetItem();

    public void SetItem(GameObject obj) => itemSocket.SetItem(obj);

    public GameObject RemoveItem() => itemSocket.RemoveItem();
}
