using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public class AppearanceData
{
    public CharacterPartType partType;
    public string id;
    public int index;

    public AppearanceData(CharacterPartType partType)
    {
        this.partType = partType;
    }
    public AppearanceData(CharacterPartType partType, string id, int index) : this(partType)
    {
        this.id = id;
        this.index = index;
    }
}

public class AppearanceManager : MonoBehaviour
{
    //References
    [Inject] private CustomizationSaveManager saveManager;
    [Inject] private CustomizationDataProvider dataProvider;
    [Inject] private OwnedItemsManager ownedItemsManager;
    [Inject] private CustomizationUIController customizationUI;

    //Dictionaries
    private Dictionary<CharacterPartType, CharacterPartHandler> characterParts = new();
    private Dictionary<CharacterPartType, AppearanceData> appearanceDataValues = new();

    private void Awake()
    {
        var parts = GetComponentsInChildren<CharacterPartHandler>();

        foreach (var part in parts)
        {
            characterParts[part.CharacterPart] = part;
        }
    }

    private void OnEnable()
    {
        saveManager.OnAppearanceDataLoaded += LoadAppearance;
        customizationUI.OnCustomizationSelectionChanged += ApplyEquipmentVariant;
        customizationUI.OnEquipRequest += SyncAppearanceDataValues;
    }

    private void OnDisable()
    {
        saveManager.OnAppearanceDataLoaded -= LoadAppearance;
        customizationUI.OnCustomizationSelectionChanged -= ApplyEquipmentVariant;
        customizationUI.OnEquipRequest -= SyncAppearanceDataValues;
    }

    private void ApplyEquipmentVariant(CharacterPartType partType, int index)
    {
        Mesh mesh = dataProvider.GetVariantMesh(partType, index);

        characterParts[partType].ApplyMesh(mesh);
    }

    private void SyncAppearanceDataValues(CharacterPartType partType, string newID, int newIndex)
    {
        if (!appearanceDataValues.ContainsKey(partType))
        {
            appearanceDataValues[partType] = new AppearanceData(partType);
        }

        AppearanceData appearanceData = appearanceDataValues[partType];
        appearanceData.id = newID;
        appearanceData.index = newIndex;

        ApplyEquipmentVariant(partType, newIndex);
    }

    private void LoadAppearance(List<AppearanceData> appearances)
    {
        if (appearances.Count > 0)
        {
            LoadAppearanceFromData(appearances);
        }

        else
        {
            ApplyDefaultAppearance();
        }

    }

    private void LoadAppearanceFromData(List<AppearanceData> appearances)
    {
        foreach (var value in appearances)
        {
            int newIndex = dataProvider.GetVariantIndex(value.partType, value.id);
            SyncAppearanceDataValues(value.partType, value.id, newIndex);
            ownedItemsManager.AddOwnedItem(value.partType, value.id);
        }
    }

    private void ApplyDefaultAppearance()
    {
        foreach (var part in characterParts)
        {
            AppearanceData appearanceData = new AppearanceData(part.Key);
            appearanceDataValues[part.Key] = appearanceData;

            if (dataProvider.EquipmentMap.TryGetValue(part.Key, out var variantMap) && appearanceData.index >= variantMap.Variants.Count)
            {
                continue;
            }

            string id = dataProvider.GetVariantID(part.Key, appearanceData.index);
            SyncAppearanceDataValues(part.Key, id, appearanceData.index);
            ownedItemsManager.AddOwnedItem(appearanceData.partType, appearanceData.id);
        }
    }

    public int GetSelectedVariantIndex(CharacterPartType partType)
    {
        if (!appearanceDataValues.ContainsKey(partType))
        {
            appearanceDataValues[partType] = new AppearanceData(partType);
        }

        return appearanceDataValues[partType].index;
    }

    public bool IsItemCurrentlyEquipped(CharacterPartType partType, string id)
    {
        return appearanceDataValues[partType].id == id;
    }
}