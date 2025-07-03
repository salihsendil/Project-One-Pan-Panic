using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Kitchem Item", menuName = "Scriptable Object/New Kitchen Item Data")]
public class KitchenItemSO : ScriptableObject
{
    public string Name;
    public KitchenItemType KitchenItemType;
    public Sprite Icon;
    public KitchenItem Prefab;
    public Mesh baseMesh;
    public KitchenItemState baseState;

    [System.Serializable]
    public class ProcessRule
    {
        public KitchenItemState inputState;
        public KitchenItemState outputState;
        public Mesh outputMesh;
        [Tooltip("How many seconds does it take to process the item?")]
        public float processTime;
    }

    public List<ProcessRule> processRules;

    public bool IsExistsAnyProcess(KitchenItemState state)
    {
        return processRules.Exists(rule => state == rule.inputState);
    }

    public bool GetProcessRuleMatch(KitchenItemState currentState, out ProcessRule newRule)
    {
        newRule = null;
        if (IsExistsAnyProcess(currentState))
        {
            newRule = processRules.Find(rule => currentState == rule.inputState);
            return true;
        };
        return false;
    }
}
