using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchem Item", menuName = "Scriptable Object/New Kitchen Item Data")]
public class KitchenItemSO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public GameObject Prefab;

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
