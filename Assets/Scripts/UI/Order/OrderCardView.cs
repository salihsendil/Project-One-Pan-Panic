using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderCardView : MonoBehaviour, IPoolable, IConfigurable<Order>
{
    //Data
    private Order currentOrder;
    private bool isCardVisible;

    //UI
    [SerializeField] private Image orderIcon;
    [SerializeField] private TMP_Text orderName;
    [SerializeField] private List<Image> recipeIngredientsIcons = new();
    [SerializeField] private ProgressBarDisplay progressBar;


    public GameObject GetGameObject() => gameObject;

    public UniversalPoolEntryType GetPoolType() => UniversalPoolEntryType.OrderCardView;

    private void OnEnable()
    {
        isCardVisible = true;
    }

    private void OnDisable()
    {
        isCardVisible = false;
    }

    public void OnSpawn()
    {
        foreach (var icon in recipeIngredientsIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }

    public void OnDespawn()
    {
        currentOrder = null;
    }

    private void Update()
    {
        if (isCardVisible && currentOrder != null)
        {
            progressBar.UpdateTimer(currentOrder.RemainingTime);
        }
    }

    public void Configure(Order order)
    {
        currentOrder = order;
        RecipeSO recipe = order.Recipe;
        orderIcon.sprite = recipe.RecipeIcon;
        orderName.text = recipe.RecipeName;

        for (int i = 0; i < recipe.Ingredients.Count; i++)
        {
            recipeIngredientsIcons[i].gameObject.SetActive(true);
            recipeIngredientsIcons[i].sprite = recipe.Ingredients[i].IngredientItemData.Icon;
        }

        progressBar.SetProgress(recipe.PreparationTime);
    }

}
