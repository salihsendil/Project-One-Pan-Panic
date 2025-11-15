using UnityEngine;

public interface IItemCarrier
{
    public bool HasItem();
    public GameObject GetItem();
    public void SetItem(GameObject obj);
    public GameObject RemoveItem();
}
