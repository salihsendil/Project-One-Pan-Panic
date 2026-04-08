using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CustomizationManager : MonoBehaviour
{
    //Zenject
    [Inject] private WardrobeManager wardrobe;
    [Inject] private CurrencyManager currencyManager;
    [Inject] private SaveSystem saveSystem;

    //Data
    [SerializeField] private BodyPartCatalogSO dataCatalog;

    //Data Mapping
    private Dictionary<BodyPartType, List<CustomizationData>> catalog = new();
    private Dictionary<BodyPartType, int> partIndices = new();

    //Worker Mapping
    private Dictionary<BodyPartType, IBodyPartFitter> bodyParts = new();

    //Navigation
    private BodyPartType currentBodyPart = BodyPartType.Body;
    private int previewIndex = 0;

    //Events
    public event Action<BodyPartType> OnChangeBodyPartChanged;
    public event Action<BuyButtonState, int?> OnClothChanged;

    private void Start()
    {
        InitializeData();
        InitializeBodyParts();
        InitializeCharacter();

        var cloth = catalog[currentBodyPart][partIndices[currentBodyPart]];
        HandleButtonState(cloth, out BuyButtonState buttonState, out int? cost);

        OnClothChanged?.Invoke(buttonState, cost);
    }

    #region Initialize

    private void InitializeData()
    {
        foreach (var item in dataCatalog.Cloths)
        {
            partIndices[item.BodyPart] = 0; //till save-load system, after that its gonna update
            catalog[item.BodyPart] = item.PartCloths.Cloths;
        }
    }

    private void InitializeBodyParts()
    {
        var parts = GetComponentsInChildren<IBodyPartFitter>();
        foreach (var part in parts)
        {
            bodyParts[part.BodyPart] = part;
        }
    }

    private void InitializeCharacter()
    {
        var equippedCloths = wardrobe.EquippedCloths;
        foreach (var key in equippedCloths.Keys)
        {
            if (catalog.TryGetValue(key, out var clothList))
            {
                CustomizationData data = clothList.Find(x => x.Id == equippedCloths[key]);
                EquipCloth(key, data);
            }

        }
    }

    #endregion

    public void OnChangeBodyPart(int step)
    {
        ApplyCloth(catalog[currentBodyPart][partIndices[currentBodyPart]]);

        int nextIndex = GetWrappedIndex((int)currentBodyPart, step, catalog.Count);
        currentBodyPart = (BodyPartType)nextIndex;
        previewIndex = partIndices[currentBodyPart];
        OnChangeBodyPartChanged?.Invoke(currentBodyPart);
        OnChangeCloth(0);
    }

    public void OnChangeCloth(int step)
    {
        int count = catalog[currentBodyPart].Count;
        previewIndex = GetWrappedIndex(previewIndex, step, count);
        var cloth = catalog[currentBodyPart][previewIndex];

        ApplyCloth(cloth);

        HandleButtonState(cloth, out BuyButtonState buttonState, out int? cost);

        OnClothChanged?.Invoke(buttonState, cost);
    }

    private void ApplyCloth(CustomizationData data)
    {
        bodyParts[currentBodyPart].Apply(data);
    }

    private void HandleButtonState(CustomizationData cloth, out BuyButtonState buttonState, out int? cost)
    {
        cost = null;

        if (!wardrobe.HasCloth(currentBodyPart, cloth.Id))
        {
            buttonState = BuyButtonState.Buy;
            cost = cloth.Cost;
        }

        else
        {
            if (!wardrobe.IsEquipped(currentBodyPart, cloth.Id))
            {
                buttonState = BuyButtonState.Equip;
                return;
            }
            buttonState = BuyButtonState.Equipped;
        }
    }

    public void HandleBuyButton()
    {
        var cloth = catalog[currentBodyPart][previewIndex];

        if (!wardrobe.HasCloth(currentBodyPart, cloth.Id))
        {
            if (currencyManager.TrySpend(cloth.Cost))
            {
                wardrobe.Unlock(currentBodyPart, cloth.Id);
                EquipCloth(currentBodyPart, cloth);
                wardrobe.Equip(currentBodyPart, cloth.Id);
                OnClothChanged?.Invoke(BuyButtonState.Equipped, null);
            }
        }

        else
        {
            if (!wardrobe.IsEquipped(currentBodyPart, cloth.Id))
            {
                EquipCloth(currentBodyPart, cloth);
                wardrobe.Equip(currentBodyPart, cloth.Id);
                OnClothChanged?.Invoke(BuyButtonState.Equipped, null);
            }
        }
    }

    private void EquipCloth(BodyPartType partType, CustomizationData cloth)
    {
        currentBodyPart = partType;
        ApplyCloth(cloth);
        partIndices[partType] = previewIndex;
    }
    #region Helper
    private int GetWrappedIndex(int index, int step, int count)
    {
        return (((index + step) % count) + count) % count;
    }
    #endregion

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        saveSystem.SaveData();
    //    }
    //}

}