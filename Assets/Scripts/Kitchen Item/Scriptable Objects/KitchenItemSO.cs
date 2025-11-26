using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KitchenItemSO", menuName = "Scriptable Objects/New KitchenItemSO")]
public class KitchenItemSO : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public Sprite icon;

    public List<ProcessRule> processRules = new();
}

[Serializable]
public class ProcessRule
{
    public ProcessType currentProcessType;
    public ProcessType nextProcessType;
    public ItemStage fromStage;
    public ItemStage toStage;
    public float processTime;
    public Mesh outputMesh;
}

