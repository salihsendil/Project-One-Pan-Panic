using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationDataProvider : MonoBehaviour
{
    [SerializeField] private CustomizationSetSO customizationSet;
    private Dictionary<CharacterPartType, EquipmentSO> equipmentMap = new();

    public Dictionary<CharacterPartType, EquipmentSO> EquipmentMap { get => equipmentMap; }

    public event Action OnCustomizationDataLoaded;

    private void Awake()
    {
        foreach (var item in customizationSet.PartSelections)
        {
            equipmentMap[item.characterPart] = item.equipment;
        }

        OnCustomizationDataLoaded?.Invoke();
    }

    public int GetVariantIndex(CharacterPartType characterPart, string id)
    {
        if (equipmentMap.TryGetValue(characterPart, out var equipment))
        {
            equipment.Variants.FindIndex(x => x.id == id);
        }

        return 0;
    }

    public string GetVariantID(CharacterPartType characterPart, int index)
    {
        if (equipmentMap.TryGetValue(characterPart, out var equipment))
        {
            return equipment.Variants[index].id;
        }
        return null;
    }

    public Mesh GetVariantMesh(CharacterPartType characterPart, int index)
    {
        if (equipmentMap.TryGetValue(characterPart, out var equipment))
        {
            return equipment.Variants[index].mesh;
        }
        return new();
    }

    public int GetVariantCost(CharacterPartType partType, int index)
    {
        if (equipmentMap.TryGetValue(partType, out var equipment))
        {
            return equipmentMap[partType].Variants[index].cost;
        }
        return 0;
    }

    public EquipmentVariant GetVariant(CharacterPartType partType, int index)
    {
        if (equipmentMap.TryGetValue(partType, out var equipment))
        {
            return equipment.Variants[index];
        }

        return null;
    }

    public int GetVariantCount(CharacterPartType partType)
    {
        if (equipmentMap.TryGetValue(partType, out EquipmentSO set))
        {
            return set.Variants.Count;
        }
        return 0;
    }

}
