using TMPro;
using UnityEngine;
using Zenject;

public class CustomizationUIController : MonoBehaviour
{
    //Zenject
    [Inject] private CurrencyManager currencyManager;

    [SerializeField] private CustomizationManager customizationManager;
    [SerializeField] private BuyButtonDisplay buyButtonDisplay;
    [SerializeField] private CurrencyDisplay currencyDisplay;

    [SerializeField] private TMP_Text categoryText;

    private void OnEnable()
    {
        currencyManager.OnCurrencyChanged += CurrencyUpdate;
        customizationManager.OnChangeBodyPartChanged += TargetBodyPartChanged;
        customizationManager.OnClothChanged += ClothChanged;
    }

    private void OnDisable()
    {
        currencyManager.OnCurrencyChanged -= CurrencyUpdate;
        customizationManager.OnChangeBodyPartChanged -= TargetBodyPartChanged;
        customizationManager.OnClothChanged -= ClothChanged;
    }

    private void Start()
    {
        CurrencyUpdate(currencyManager.CurrentCurrency);
    }

    private void TargetBodyPartChanged(BodyPartType type)
    {
        categoryText.text = type.ToString();
    }

    private void ClothChanged(BuyButtonState state, int? cost)
    {
        string text = state == BuyButtonState.Buy ? cost.ToString() : state.ToString();

        buyButtonDisplay.UpdateButtonText(text);
    }

    private void CurrencyUpdate(int newCurrency)
    {
        currencyDisplay.UpdateCurrencyText(newCurrency.ToString());
    }
}
