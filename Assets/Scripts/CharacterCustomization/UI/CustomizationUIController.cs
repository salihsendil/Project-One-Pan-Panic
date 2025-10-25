using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CustomizationUIController : MonoBehaviour
{
    //References
    [Inject] private CustomizationDataProvider dataProvider;
    [Inject] private OwnedItemsManager ownedItemsManager;
    [Inject] private AppearanceManager appearanceManager;

    //Events
    public event Action<CharacterPartType, int> OnVariantChanged; //güncellencek

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
    private int currentVariantIndex = 0;
    private int currentVariantCount;

    [Header("Variant Item")]
    private bool isCurrentItemOwned;

    [Header("Test Buttons")]
    [Inject] CustomizationSaveManager saveManager;
    [SerializeField] private Button saveAppearanceButton;
    [SerializeField] private Button loadAppearanceButton;

    private void OnEnable()
    {
        //Event Invokes
        dataProvider.OnCustomizationDataLoaded += InitializeCustomizationDataCache;
        saveManager.OnAllDataLoaded += InitializeUIElements;

        //Button Actions
        BindUIButtons();
        equipmentButton.onClick.AddListener(() => InvokeEquipButtonEvent(isCurrentItemOwned));

        //TEST EVENTS
        saveAppearanceButton.onClick.AddListener(() => SaveAppearanceEvent());
        loadAppearanceButton.onClick.AddListener(() => LoadAppearanceEvent());
    }

    private void OnDisable()
    {
        //Event Invokes
        dataProvider.OnCustomizationDataLoaded -= InitializeCustomizationDataCache;
        saveManager.OnAllDataLoaded -= InitializeUIElements;

        //Button Actions
        UnBindUIButtons();
        equipmentButton.onClick.RemoveListener(() => InvokeEquipButtonEvent(isCurrentItemOwned));

        //TEST EVENTS
        saveAppearanceButton.onClick.RemoveListener(() => SaveAppearanceEvent());
        loadAppearanceButton.onClick.RemoveListener(() => LoadAppearanceEvent());
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
        customizationSetRightButton.onClick.AddListener(() => OnRightCustomizationSetButton());
        customizationSetLeftButton.onClick.AddListener(() => OnLeftCustomizationSetButton());
        #endregion

        #region Customization Variant Buttons
        equipmentRightButton.onClick.AddListener(() => OnRightEquipmentVariantButton());
        equipmentLeftButton.onClick.AddListener(() => OnLeftEquipmentVariantButton());
        #endregion
    }

    private void UnBindUIButtons()
    {
        #region Customization Set Buttons
        customizationSetRightButton.onClick.RemoveListener(() => OnRightCustomizationSetButton());
        customizationSetLeftButton.onClick.RemoveListener(() => OnLeftCustomizationSetButton());
        #endregion

        #region Customization Variant Buttons
        equipmentRightButton.onClick.RemoveListener(() => OnRightEquipmentVariantButton());
        equipmentLeftButton.onClick.RemoveListener(() => OnLeftEquipmentVariantButton());
        #endregion
    }

    private void OnRightCustomizationSetButton() => SwithCustomizationSet(true);
    private void OnLeftCustomizationSetButton() => SwithCustomizationSet(false);
    private void SwithCustomizationSet(bool forward)
    {
        CharacterPartType partType = customizationSet[currentSetIndex];
        OnVariantChanged?.Invoke(partType, appearanceManager.GetSelectedVariantIndex(partType));
        currentSetIndex = GetNextIndex(currentSetIndex, customizationSet.Count, forward);
        currentVariantIndex = appearanceManager.GetSelectedVariantIndex(partType);
        currentVariantCount = dataProvider.GetVariantCount(customizationSet[currentSetIndex]);
        customizationSetText.text = customizationSet[currentSetIndex].ToString();
        RefreshEquipmentButtonVisual();
    }


    private void OnRightEquipmentVariantButton() => SwitchEquipmentVariant(true);
    private void OnLeftEquipmentVariantButton() => SwitchEquipmentVariant(false);
    private void SwitchEquipmentVariant(bool forward)
    {
        currentVariantIndex = GetNextIndex(currentVariantIndex, currentVariantCount, forward);
        RefreshEquipmentButtonVisual();
        OnVariantChanged?.Invoke(customizationSet[currentSetIndex], currentVariantIndex);
    }

    private void RefreshEquipmentButtonVisual()
    {
        CharacterPartType partType = customizationSet[currentSetIndex];
        string id = dataProvider.GetVariantID(partType, currentVariantIndex);
        isCurrentItemOwned = ownedItemsManager.IsOwnedItem(partType, id);

        if (isCurrentItemOwned)
        {
            equipmentLockedImage.enabled = false;
            if (appearanceManager.IsItemCurrentlyEquipped(partType, id))
            {
                equipmentButtonText.text = "Equipped";
                equipmentButton.interactable = false;
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
            equipmentButtonText.text = dataProvider.GetVariantCost(partType, currentVariantIndex).ToString();
        }
    }

    private void InvokeEquipButtonEvent(bool isOwned)
    {
        CharacterPartType partType = customizationSet[currentSetIndex];
        EquipmentVariant variant = dataProvider.GetVariant(partType, currentVariantIndex);

        if (isOwned)
        {
            OnEquipRequest?.Invoke(partType, variant.id, currentVariantIndex);
        }

        else
        {
            OnPurchaseRequested?.Invoke(partType, variant.id, variant.cost);
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

    //Test Methods
    private void SaveAppearanceEvent()
    {
        saveManager.SaveAppearanceData(appearanceManager.AppearanceDataValues.Values.ToList());
    }

    private void LoadAppearanceEvent()
    {
        saveManager.LoadAppearanceData();
    }
}
