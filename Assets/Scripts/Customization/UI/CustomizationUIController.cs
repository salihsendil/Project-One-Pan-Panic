using TMPro;
using UnityEngine;
using Zenject;

public class CustomizationUIController : MonoBehaviour
{
    //Zenject
    [Inject] private CurrencyManager currencyManager;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CustomizationManager customizationManager;
    [SerializeField] private BuyButtonDisplay buyButtonDisplay;
    [SerializeField] private CurrencyDisplay currencyDisplay;

    [SerializeField] private TMP_Text categoryText;

    private void OnEnable()
    {
        currencyManager.OnCurrencyChanged += CurrencyUpdate;
        customizationManager.OnBodyPartChanged += BodyPartChanged;
        customizationManager.OnClothChanged += ClothChanged;
    }

    private void OnDisable()
    {
        currencyManager.OnCurrencyChanged -= CurrencyUpdate;
        customizationManager.OnBodyPartChanged -= BodyPartChanged;
        customizationManager.OnClothChanged -= ClothChanged;
    }

    public void SetVisibility(bool isOpen)
    {
        if (!isOpen)
            customizationManager.RevertAllChanges();

        canvasGroup.interactable = isOpen;
        canvasGroup.blocksRaycasts = isOpen;

        int visiblity = isOpen ? 1 : 0;
        canvasGroup.alpha = visiblity;
    }

    private void Start()
    {
        CurrencyUpdate(currencyManager.CurrentCurrency);
    }

    private void BodyPartChanged(BodyPartType type)
    {
        categoryText.text = type.ToString();
    }

    private void ClothChanged(string text)
    {
        buyButtonDisplay.UpdateButtonText(text);
    }

    private void CurrencyUpdate(int newCurrency)
    {
        currencyDisplay.UpdateCurrencyText(newCurrency.ToString());
    }
}
