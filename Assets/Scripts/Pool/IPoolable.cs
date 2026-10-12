using UnityEngine;

public interface IPoolable
{
    public GameObject GameObject { get; }
    public ItemType ItemType { get; }

    public void Spawn();
    public void Despawn();
}