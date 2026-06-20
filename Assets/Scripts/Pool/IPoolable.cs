using System;
using UnityEngine;

public interface IPoolable
{
    public GameObject GetGameObject { get; }
    public ItemType GetPoolType { get; }

    //public event Action OnSpawn; //required
    //public event Action OnDespawn; //required

    public void Spawn();
    public void Despawn();
}