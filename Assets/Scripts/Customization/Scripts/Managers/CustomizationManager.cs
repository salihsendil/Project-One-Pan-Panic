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
    private Dictionary<BodyPartType, List<ClothData>> catalog = new();
    private Dictionary<BodyPartType, int> partIndices = new();

    //Worker Mapping
    private Dictionary<BodyPartType, BodyPartFitter> bodyParts = new();

    //Navigation
    private BodyPartType currentBodyPart = BodyPartType.Accessories;
    private int previewIndex = 0;

    //Events
    public event Action<BodyPartType> OnChangeBodyPartChanged;
    public event Action<BuyButtonState, int?> OnClothChanged;

    private void Start()
    {
        InitializeData();
        InitializeBodyParts();

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
        var parts = GetComponentsInChildren<BodyPartFitter>();

        foreach (var part in parts)
        {
            bodyParts[part.BodyPart] = part;
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

    private void ApplyCloth(ClothData cloth)
    {
        Mesh newMesh = cloth.Mesh;
        bodyParts[currentBodyPart].UpdateMesh(newMesh);
    }

    private void HandleButtonState(ClothData cloth, out BuyButtonState buttonState, out int? cost)
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
                OnClothChanged?.Invoke(BuyButtonState.Equipped, null);
            }
        }

        else
        {
            if (!wardrobe.IsEquipped(currentBodyPart, cloth.Id))
            {
                EquipCloth(currentBodyPart, cloth);
                OnClothChanged?.Invoke(BuyButtonState.Equipped, null);
            }
        }
    }

    private void EquipCloth(BodyPartType partType, ClothData cloth)
    {
        ApplyCloth(cloth);
        wardrobe.Equip(partType, cloth.Id);
        partIndices[partType] = previewIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            saveSystem.SaveData();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            saveSystem.LoadData();
        }
    }

    #region Helper
    private int GetWrappedIndex(int index, int step, int count)
    {
        return (((index + step) % count) + count) % count;
    }
    #endregion
}