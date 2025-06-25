using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class OrderDisplay : MonoBehaviour
{
    [SerializeField] private OrderInstance order;
    [SerializeField] private TMP_Text orderName;
    [SerializeField] private List<Image> ingredientIcons = new List<Image>();
    [SerializeField] private float orderTimer;
    [SerializeField] private Image timerSlider;

    public OrderInstance Order { get => order; }
    public Image Image { get; private set; }
    public RectTransform RectTransform { get; private set; }

    private void Awake()
    {
        Image = GetComponent<Image>();

        RectTransform = GetComponent<RectTransform>();

        HideOrderPanel();
    }

    public void ShowOrderPanel(OrderInstance newOrder)
    {
        order = newOrder;

        orderName.alpha = 1f;
        orderName.SetText(order.RecipeSO.name);

        for (int i = 0; i < order.RecipeSO.recipeIngredients.Count; i++)
        {
            ingredientIcons[i].color = new Color(ingredientIcons[i].color.r, ingredientIcons[i].color.g, ingredientIcons[i].color.b, 1f);
            ingredientIcons[i].sprite = order.RecipeSO.recipeIngredients[i].kitchenItemSO.Icon;
        }

        timerSlider.color = new Color(timerSlider.color.r, timerSlider.color.g, timerSlider.color.b, 1f);
        orderTimer = order.RecipeSO.preparationTime;
        timerSlider.fillAmount = 1f;
    }

    public void HideOrderPanel()
    {
        orderName.alpha = 0f;

        for (int i = 0; i < ingredientIcons.Count; i++)
        {
            ingredientIcons[i].color = new Color(ingredientIcons[i].color.r, ingredientIcons[i].color.g, ingredientIcons[i].color.b, 0f);
        }

        timerSlider.color = new Color(timerSlider.color.r, timerSlider.color.g, timerSlider.color.b, 0f);
    }

    private void Update()
    {
        if (order != null)
        {
            timerSlider.fillAmount = order.RemainingTime / orderTimer;
        }
    }
}
