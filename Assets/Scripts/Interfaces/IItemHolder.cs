using UnityEngine;

public interface IItemHolder
{
    public bool HasItem();
    public GameObject GetItem();
    public void SetItem(GameObject obj);
    public GameObject RemoveItem();
}
