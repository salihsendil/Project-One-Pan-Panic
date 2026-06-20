using UnityEngine;

public interface IPickable
{
    public Transform Transform { get; }
    public GameObject GetGameObject { get; }
    public ItemType GetItemType();
    public bool IsPickable { get; set; }
}
