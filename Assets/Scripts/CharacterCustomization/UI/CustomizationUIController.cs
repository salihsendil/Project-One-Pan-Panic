using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationUIController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private int currentCategoryIndex;
    [SerializeField] private int currentVariantIndex;

    [Header("Category")]
    [SerializeField] private TMP_Text categoryText;
    [SerializeField] private Button leftCategoryBttn;
    [SerializeField] private Button rightCategoryBttn;

    [Header("Variants")]
    [SerializeField] private Button leftVariantBttn;
    [SerializeField] private Button rightVariantBttn;

    [Header("Buy-Equip")]
    [SerializeField] private TMP_Text buyCostText;

    private void Awake()
    {
        currentCategoryIndex = currentVariantIndex = 0;
    }

    private void Start()
    {
        SetCategory(currentCategoryIndex);
        NavButtonClick(categoryStep: 0, variantStep: 0);
        SetBuyButton(CustomizationDataManager.Instance.Data.categories[currentCategoryIndex].category.variants[currentVariantIndex].cost);

        rightCategoryBttn.onClick.AddListener(() => NavButtonClick(categoryStep: 1));
        leftCategoryBttn.onClick.AddListener(() => NavButtonClick(categoryStep: -1));

        rightVariantBttn.onClick.AddListener(() => NavButtonClick(variantStep: 1));
        leftVariantBttn.onClick.AddListener(() => NavButtonClick(variantStep: -1));

    }

    private void SetCategory(int index)
    {
        categoryText.text = CustomizationDataManager.Instance.Data.categories[index].bodyPart.ToString();
    }

    public void NavButtonClick(int? categoryStep = null, int? variantStep = null)
    {
        if (categoryStep.HasValue)
        {
            currentCategoryIndex += categoryStep.Value;
            if (currentCategoryIndex >= CustomizationDataManager.Instance.Data.categories.Count)
            {
                currentCategoryIndex = 0;
            }
            else if (currentCategoryIndex < 0)
            {
                currentCategoryIndex = CustomizationDataManager.Instance.Data.categories.Count - 1;
            }

            currentVariantIndex = 0;
            SetCategory(currentCategoryIndex);
        }

        if (variantStep.HasValue)
        {
            currentVariantIndex += variantStep.Value;
            if (currentVariantIndex >= CustomizationDataManager.Instance.Data.categories[currentCategoryIndex].category.variants.Count)
            {
                currentVariantIndex = 0;
            }

            else if (currentVariantIndex < 0)
            {
                currentVariantIndex = CustomizationDataManager.Instance.Data.categories[currentCategoryIndex].category.variants.Count - 1;
            }
            Debug.Log($"current variant index: {currentVariantIndex}," +
                $" item category{CustomizationDataManager.Instance.Data.categories[currentCategoryIndex].category.name} ," +
                $" item name {CustomizationDataManager.Instance.Data.categories[currentCategoryIndex].category.variants[currentVariantIndex].mesh.name}");

            CharacterCustomizer.Instance.ApplyData(CustomizationDataManager.Instance.Data.categories[currentCategoryIndex], currentVariantIndex);
        }

        SetBuyButton(CustomizationDataManager.Instance.Data.categories[currentCategoryIndex].category.variants[currentVariantIndex].cost);
    }

    private void SetBuyButton(int cost)
    {
        buyCostText.text = cost.ToString();
    }

}
