using UnityEngine;


// Main contract for transferable items. All transferable items must implement this.


public interface IPickable
{
    public bool IsPickable { get; set; }
    public ItemType ItemType { get; }
    public Transform Transform { get; }
}