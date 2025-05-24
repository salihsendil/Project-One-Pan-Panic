using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen Object", menuName = "Scriptable Objects/New Kitchen Object")]
public class KitchenObjectSO : ScriptableObject
{
    public string objectName;
    public Sprite objectIcon;
    public GameObject objectPrefab;
    public KitchenObjectState kitchenObjectState;

    [System.Serializable]
    public class ProcessRule
    {
        public KitchenStationType kitchenStationType;
        public KitchenObjectState inputKitchenObjectState;
        public KitchenObjectState outputKitchenObjectState;
        public Mesh outputKitchenObjectMesh;
        [Tooltip("How many seconds the process will take? Eg: if it takes 3 seconds to cut a tomato, input 3.")]
        public float processTimer;
        [Tooltip("How many steps the process will take? Eg: tomato is sliced in one step.")]
        public int processStepCounter;
    }

    public List<ProcessRule> processRules;

    public bool IsProcessableAtStation(KitchenStationType stationType, KitchenObjectState objectState)
    {
        return processRules.Exists(rule => rule.kitchenStationType == stationType && rule.inputKitchenObjectState == objectState);
    }

    public KitchenObjectState GetProcessedState(KitchenStationType stationType, KitchenObjectState objectState)
    {
        var rule = processRules.Find(r =>
            r.kitchenStationType == stationType && r.inputKitchenObjectState == objectState);

        if (rule == null)
        {
            throw new System.Exception(
                $"No process rule found for station '{stationType}' with input state '{objectState}'");
        }

        return rule.outputKitchenObjectState;
    }

    public ProcessRule GetMatch(KitchenStationType stationType, KitchenObjectState objectState)
    {
        return processRules.Find(rule => stationType == rule.kitchenStationType && objectState == rule.inputKitchenObjectState); 
    }

}
