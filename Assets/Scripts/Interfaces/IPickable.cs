using UnityEngine;

public interface IPickable
{
    public Transform Transform { get; }
    public GameObject GetGameObject { get; }
    public bool IsPickable { get; set; }
}
