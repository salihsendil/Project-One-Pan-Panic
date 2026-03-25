using UnityEngine;

public interface IPoolable
{
    public GameObject GetGameObject { get; }
    public UniversalPoolEntryType GetPoolType { get; }
    public void OnSpawn();
    public void OnDespawn();
}