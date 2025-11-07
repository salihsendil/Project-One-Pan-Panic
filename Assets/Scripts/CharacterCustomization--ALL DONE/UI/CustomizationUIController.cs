using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CustomizationUIController : MonoBehaviour
{
    //References
    [Inject] private readonly CustomizationSaveManager saveManager;
    [Inject] private readonly CustomizationDataProvider dataProvider;
    [Inject] private readonly OwnedItemsManager ownedItemsManager;
    [Inject] private readonly AppearanceManager appearanceManager;

    //Events
    public event Action<CharacterPartType, int> OnCustomizationSelectionChanged;
    public event Action<CharacterPartType, string, int> OnEquipRequest;
    public event Action<CharacterPartType, string, int> OnPurchaseRequested;

    [Header("Customization Set")]
    [SerializeField] private TMP_Text customizationSetText;
    [SerializeField] private Button customizationSetRightButton;
    [SerializeField] private Button customizationSetLeftButton;


    [Header("Customization Equipment")]
    [SerializeField] private Button equipmentButton;
    [SerializeField] private TMP_Text equipmentButtonText;
    [SerializeField] private Image equipmentLockedImage;
    [SerializeField] private Button equipmentRightButton;
    [SerializeField] private Button equipmentLeftButton;

    [Header("Data")]
    private List<CharacterPartType> customizationSet = new();
    private int currentSetIndex = 0;
    private CharacterPartType currentPartType = CharacterPartType.Accessories;
    private int currentVariantIndex = 0;
    private int currentVariantCount;

    [Header("Variant Item")]
    private bool isCurrentItemOwned;

    [Header("Test Buttons")]
    [SerializeField] private Button saveAppearanceButton;
    [SerializeField] private Button loadAppearanceButton;

    private void OnEnable()
    {
        //Events
        dataProvider.OnCustomizationDataLoaded += InitializeCustomizationDataCache;
        saveManager.OnAllDataLoaded += InitializeUIElements;

        //Button Actions
        BindUIButtons();
    }

    private void OnDisable()
    {
        //Events
        dataProvider.OnCustomizationDataLoaded -= InitializeCustomizationDataCache;
        saveManager.OnAllDataLoaded -= InitializeUIElements;

        //Button Actions
        UnBindUIButtons();
    }

    private void InitializeCustomizationDataCache()
    {
        var equipmentsList = dataProvider.EquipmentMap.Keys;
        foreach (var key in equipmentsList)
        {
            customizationSet.Add(key);
        }

        currentVariantCount = dataProvider.GetVariantCount(customizationSet[currentSetIndex]);
    }

    private void InitializeUIElements()
    {
        RefreshEquipmentButtonVisual();
        customizationSetText.text = customizationSet[currentSetIndex].ToString();
    }

    private void BindUIButtons()
    {
        #region Customization Set Buttons
        customizationSetRightButton.onClick.AddListener(OnRightCustomizationSetButton);
        customizationSetLeftButton.onClick.AddListener(OnLeftCustomizationSetButton);
        #endregion

        #region Customization Variant Buttons
        equipmentRightButton.onClick.AddListener(OnRightEquipmentVariantButton);
        equipmentLeftButton.onClick.AddListener(OnLeftEquipmentVariantButton);
        #endregion

        #region Equipment Button
        equipmentButton.onClick.AddListener(OnEquipmentButtonPressed);
        #endregion
    }

    private void UnBindUIButtons()
    {
        #region Customization Set Buttons
        customizationSetRightButton.onClick.RemoveListener(OnRightCustomizationSetButton);
        customizationSetLeftButton.onClick.RemoveListener(OnLeftCustomizationSetButton);
        #endregion

        #region Customization Variant Buttons
        equipmentRightButton.onClick.RemoveListener(OnRightEquipmentVariantButton);
        equipmentLeftButton.onClick.RemoveListener(OnLeftEquipmentVariantButton);
        #endregion

        #region Equipment Button
        equipmentButton.onClick.RemoveListener(OnEquipmentButtonPressed);
        #endregion
    }


    private void OnRightCustomizationSetButton() => SwitchCustomizationSet(true);
    private void OnLeftCustomizationSetButton() => SwitchCustomizationSet(false);
    private void SwitchCustomizationSet(bool forward)
    {
        if (!appearanceManager.IsItemCurrentlyEquipped(currentPartType, dataProvider.GetVariantID(currentPartType, currentVariantIndex)))
        {
            OnCustomizationSelectionChanged?.Invoke(currentPartType, appearanceManager.GetSelectedVariantIndex(currentPartType));
        }

        currentSetIndex = GetNextIndex(currentSetIndex, customizationSet.Count, forward);
        currentPartType = customizationSet[currentSetIndex];
        currentVariantCount = dataProvider.GetVariantCount(currentPartType);
        currentVariantIndex = appearanceManager.GetSelectedVariantIndex(currentPartType);
        customizationSetText.text = currentPartType.ToString();
        OnCustomizationSelectionChanged?.Invoke(currentPartType, currentVariantIndex);
        RefreshEquipmentButtonVisual();
    }


    private void OnRightEquipmentVariantButton() => SwitchEquipmentVariant(true);
    private void OnLeftEquipmentVariantButton() => SwitchEquipmentVariant(false);
    private void SwitchEquipmentVariant(bool forward)
    {
        currentVariantIndex = GetNextIndex(currentVariantIndex, currentVariantCount, forward);
        OnCustomizationSelectionChanged?.Invoke(currentPartType, currentVariantIndex);
        RefreshEquipmentButtonVisual();
    }

    private void RefreshEquipmentButtonVisual()
    {
        string id = dataProvider.GetVariantID(customizationSet[currentSetIndex], currentVariantIndex);
        isCurrentItemOwned = ownedItemsManager.IsOwnedItem(currentPartType, id);
        if (isCurrentItemOwned)
        {
            equipmentLockedImage.enabled = false;
            if (appearanceManager.IsItemCurrentlyEquipped(currentPartType, id))
            {
                equipmentButton.interactable = false;
                equipmentButtonText.text = "Equipped";
            }

            else
            {
                equipmentButton.interactable = true;
                equipmentButtonText.text = "Equip";
            }
        }

        else
        {
            equipmentButton.interactable = true;
            equipmentLockedImage.enabled = true;
            equipmentButtonText.text = dataProvider.GetVariantCost(currentPartType, currentVariantIndex).ToString();
        }
    }


    private void OnEquipmentButtonPressed() => HandleEquipmentButtonPressed(isCurrentItemOwned);
    private void HandleEquipmentButtonPressed(bool isOwned)
    {
        EquipmentVariant variant = dataProvider.GetVariant(currentPartType, currentVariantIndex);

        if (isOwned)
        {
            OnEquipRequest?.Invoke(currentPartType, variant.id, currentVariantIndex);
        }

        else
        {
            OnPurchaseRequested?.Invoke(currentPartType, variant.id, variant.cost);
        }

        RefreshEquipmentButtonVisual();
    }

    private int GetNextIndex(int current, int count, bool forward)
    {
        if (forward)
        {
            return (current + 1) % count;
        }

        else
        {
            return (current - 1 + count) % count;
        }
    }
}