using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CustomizationManager : MonoBehaviour
{
    //Data Model
    public struct BodyPartBinding
    {
        private int equippedIndex;

        //public int EquippedIndex;
        public IBodyPartFitter BodyPartFitter;

        public int EquippedIndex{ get => equippedIndex; set => equippedIndex = value; }

        public BodyPartBinding(int equippedIndex, IBodyPartFitter bodyPartFitter)
        {
            this.equippedIndex = equippedIndex;
            BodyPartFitter = bodyPartFitter;
        }

        public void UpdateIndex(int equippedIndex)
        {
            this.equippedIndex = equippedIndex;
        }
    }

    //Zenject
    [Inject] private WardrobeManager wardrobe;
    [Inject] private CurrencyManager currencyManager;

    //Data
    [SerializeField] private BodyPartCatalogSO catalogSO;

    //Data Mapping
    private Dictionary<BodyPartType, List<CustomizationData>> catalog = new();
    private Dictionary<BodyPartType, BodyPartBinding> bodyParts = new();

    //Navigation
    private BodyPartType currentPart = BodyPartType.Body;
    private int previewIndex;

    //Events
    public event Action<BodyPartType> OnBodyPartChanged;
    public event Action<string> OnClothChanged;

    private void Start()
    {
        InitializeCatalog();
        InitializeCharacter();
        ChangeBodyPart(0);
    }

    private void InitializeCatalog()
    {
        foreach (var entry in catalogSO.Catalog)
        {
            catalog[entry.BodyPart] = entry.PartCloths.ClothsList;
        }
    }
    private void InitializeCharacter()
    {
        var parts = GetComponentsInChildren<IBodyPartFitter>();

        foreach (var part in parts)
        {
            bodyParts[part.BodyPart] = new BodyPartBinding(0, part);

            string clothId = wardrobe.GetEquippedClothId(part.BodyPart);

            int newIndex = catalog[part.BodyPart].FindIndex(x => x.Id == clothId);
            if (newIndex == -1) continue;

            BodyPartBinding partBinding = bodyParts[part.BodyPart];
            partBinding.UpdateIndex(newIndex);
            bodyParts[part.BodyPart] = partBinding;

            EquipCloth(part.BodyPart, catalog[part.BodyPart][newIndex]);
        }
    }


    public void ChangeBodyPart(int stepSize)
    {
        RevertChanges(currentPart);

        currentPart = (BodyPartType)GetWrappedIndex((int)currentPart, stepSize, catalog.Count);
        previewIndex = bodyParts[currentPart].EquippedIndex;

        ChangeCloth(0);
        OnBodyPartChanged?.Invoke(currentPart);
    }

    public void ChangeCloth(int stepSize)
    {
        int catalogSize = catalog[currentPart].Count;

        previewIndex = GetWrappedIndex(previewIndex, stepSize, catalogSize);
        CustomizationData data = catalog[currentPart][previewIndex];

        EquipCloth(currentPart, data);
        HandleButtonState(data);
    }

    private void HandleButtonState(CustomizationData data)
    {
        string buttonText;

        if (wardrobe.IsEquipped(currentPart, data.Id))
            buttonText = BuyButtonState.Equipped.ToString();

        else if (wardrobe.HasCloth(currentPart, data.Id))
            buttonText = BuyButtonState.Equip.ToString();

        else
            buttonText = data.Cost.ToString();

        OnClothChanged?.Invoke(buttonText);
    }

    public void HandleBuyButton()
    {
        CustomizationData data = catalog[currentPart][previewIndex];

        if (!wardrobe.HasCloth(currentPart, data.Id))
        {
            if (!currencyManager.TrySpend(data.Cost)) return;
            wardrobe.Unlock(currentPart, data.Id);
        }
        wardrobe.Equip(currentPart, data.Id);

        BodyPartBinding partBinding = bodyParts[currentPart];
        partBinding.UpdateIndex(previewIndex);
        bodyParts[currentPart] = partBinding;

        EquipCloth(currentPart, data);
        HandleButtonState(data);
    }

    private void EquipCloth(BodyPartType partType, CustomizationData data)
    {
        bodyParts[partType].BodyPartFitter.Apply(data);
    }

    #region Helper
    private int GetWrappedIndex(int index, int step, int count)
    {
        return (((index + step) % count) + count) % count;
    }

    private void RevertChanges(BodyPartType partType)
    {
        int index = bodyParts[partType].EquippedIndex;
        CustomizationData data = catalog[partType][index];

        EquipCloth(partType, data);
    }

    public void RevertAllChanges()
    {
        foreach (var part in bodyParts.Keys)
        {
            RevertChanges(part);
        }
    }
    #endregion
}