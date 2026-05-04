using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UniversalPoolManager : MonoBehaviour
{
    //Zenject
    [Inject] private IInstantiator instantiator;
    [Inject] private UniversalPoolConfigSO poolConfig;

    //Pool
    private Dictionary<UniversalPoolEntryType, UniversalPoolEntry> poolEntries = new();
    private Dictionary<UniversalPoolEntryType, Stack<GameObject>> pool = new();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (var entry in poolConfig.PoolEntries)
        {
            poolEntries[entry.Type] = entry;

            if (!pool.TryGetValue(entry.Type, out var stack))
            {
                stack = new Stack<GameObject>();
                pool[entry.Type] = stack;
            }

            for (int i = 0; i < entry.InitializeSize; i++)
            {
                GameObject go = instantiator.InstantiatePrefab(entry.Prefab, transform);
                go.SetActive(false);
                stack.Push(go);
            }
        }
    }

    public GameObject Spawn(UniversalPoolEntryType type)
    {
        if (!pool.TryGetValue(type, out var stack))
        {
            stack = new Stack<GameObject>();
            pool[type] = stack;
        }

        if (!stack.TryPop(out GameObject go))
        {
            if (!poolEntries.TryGetValue(type, out var entry)) { return null; }

            if (!entry.HasHardLimit)
            {
                go = instantiator.InstantiatePrefab(entry.Prefab);
            }
        }
        go?.SetActive(true);
        return go;
    }

    public T Spawn<T>(UniversalPoolEntryType type) where T : Component, IPoolable
    {
        GameObject go = Spawn(type);

        if (go == null || !go.TryGetComponent(out T component)) { return null; }
        component.Spawn();
        return component;
    }

    public T SpawnWithConfig<T, TParam>(UniversalPoolEntryType type, TParam config)
                                         where T : Component, IPoolable, IConfigurable<TParam>
    {
        T component = Spawn<T>(type);

        if (component == null) { return null; }

        component.Configure(config);

        return component;
    }

    public void Despawn(IPoolable poolable)
    {
        if (poolable == null) { return; }

        if (!pool.TryGetValue(poolable.GetPoolType, out var stack))
        {
            stack = new Stack<GameObject>();
            pool[poolable.GetPoolType] = stack;
        }

        poolable.Despawn();

        GameObject go = poolable.GetGameObject;
        go.SetActive(false);
        go.transform.SetParent(transform);
        go.transform.position = Vector3.zero;

        stack.Push(go);
    }

    public void ClearPool(UniversalPoolEntryType type)
    {
        if (!pool.TryGetValue(type, out var stack)) { return; }

        stack.Clear();
        pool.Remove(type);
    }
}