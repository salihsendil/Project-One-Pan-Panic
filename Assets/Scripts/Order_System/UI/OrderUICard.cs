using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUICard : MonoBehaviour
{
    [SerializeField] private Order currentOrder;

    [Header("Card")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image background;
    [SerializeField] private Color32 orderExpiredColor;
    [SerializeField] private Color32 orderDeliveredColor;

    [Header("Icons")]
    [SerializeField] private Image recipeIcon;
    [SerializeField] private List<Image> ingredientIcon = new();

    [Header("Time Bar")]
    [SerializeField] private Image timeBar;

    [Header("Time Bar Shake Tween")]
    [SerializeField] private bool isShaking;
    [SerializeField] private float timeBarShakeThreshold = 0.3f;

    [Header("Time Bar Colors")]
    [SerializeField] private Color32 baseColor;
    [SerializeField] private Color32 warningColor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        background = GetComponent<Image>();
    }

    private void Update()
    {
        TickTimeBar();
    }

    private void TickTimeBar()
    {
        timeBar.fillAmount = currentOrder.RemainingRatio;
        timeBar.color = Color.Lerp(warningColor, baseColor, currentOrder.RemainingRatio);

        if (!isShaking && currentOrder.RemainingRatio <= timeBarShakeThreshold)
        {
            StartShakePanel();
        }
    }

    private void StartShakePanel()
    {
        isShaking = true;
        rectTransform.DOShakeAnchorPos(0.5f, 5f, 10, 60).SetLoops(-1, LoopType.Restart);
    }

    private void StopShakePanel()
    {
        rectTransform.DOKill();
        isShaking = false;
    }

    private void OrderExpired()
    {
        rectTransform.DOShakeAnchorPos(0.6f, 40f, 60, 90, true);
        background.DOColor(orderExpiredColor, 0.1f).SetLoops(6, LoopType.Yoyo);
        timeBar.fillAmount = 1;
    }

    public void Initialize(Order order)
    {
        currentOrder = order;
        RecipeSO recipe = order.Recipe;


        recipeIcon.enabled = false;
        recipeIcon.sprite = recipe.Icon;

        for (int i = 0; i < recipe.Ingredients.Count; i++)
        {
            ingredientIcon[i].enabled = true;
            ingredientIcon[i].sprite = recipe.Ingredients[i].IngredientSO.Icon;
        }

        timeBar.fillAmount = 1;
        gameObject.SetActive(true);
        currentOrder.OnOrderSetAgain += OrderExpired;
    }

    public void Disable()
    {
        StopShakePanel();
        gameObject.SetActive(false);

        recipeIcon.enabled = false;

        foreach (var icon in ingredientIcon)
        {
            icon.enabled = false;
        }

        currentOrder.OnOrderSetAgain -= OrderExpired;
        currentOrder = null;
    }
}