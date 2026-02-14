using UnityEngine;

public interface IPoolable
{
    public GameObject GetGameObject();
    public UniversalPoolEntryType GetPoolType();
    public void OnSpawn();
    public void OnDespawn();
}