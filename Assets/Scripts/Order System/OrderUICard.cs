using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OrderUICard : MonoBehaviour, IPoolable, IConfigurable<Order>
{
    private Order currentOrder;

    [SerializeField] private Image visualBackground;
    [SerializeField] private RectTransform visualPanel;

    [SerializeField] private Image orderIcon;
    [SerializeField] private List<Image> ingredientIcons;
    [SerializeField] private OrderCardProcessDisplay processDisplay;
    [SerializeField] private Color32 orderExpiredColor;
    [SerializeField] private Color32 orderDeliveredColor;


    private void Awake()
    {
        processDisplay = GetComponentInChildren<OrderCardProcessDisplay>();
    }

    #region IPoolable

    public GameObject GetGameObject => gameObject;

    public ItemType GetPoolType => ItemType.OrderCard;

    public void Spawn()
    {
        orderIcon.gameObject.SetActive(false);

        foreach (var icon in ingredientIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }

    public void Despawn()
    {
        currentOrder.OnOrderSetAgain -= OrderSetAgain;
        processDisplay.Disable();
        currentOrder = null;
    }

    #endregion

    #region IConfigurable

    public void Configure(Order order)
    {
        orderIcon.gameObject.SetActive(true);
        orderIcon.sprite = order.Recipe.RecipeIcon;

        for (int i = 0; i < order.Recipe.Ingredients.Count; i++)
        {
            ingredientIcons[i].gameObject.SetActive(true);
            ingredientIcons[i].sprite = order.Recipe.Ingredients[i].IngredientItemData.Icon;
        }

        processDisplay.Set(order.Recipe.PreparationTime);

        currentOrder = order;
        currentOrder.OnOrderSetAgain += OrderSetAgain;
    }

    #endregion

    private void Update()
    {
        if (currentOrder == null) return;

        processDisplay.Tick(currentOrder.Time);
    }

    private void OrderSetAgain()
    {
        visualPanel.DOShakeAnchorPos(0.6f, 40f, 60, 90, true);
        visualBackground.DOColor(orderExpiredColor, 0.1f).SetLoops(6, LoopType.Yoyo);

        if (currentOrder == null) return;
        processDisplay.Set(currentOrder.Recipe.PreparationTime);
    }

    public async Task OrderDeliveredAsync()
    {
        await visualBackground.DOColor(orderDeliveredColor, 0.4f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion();
    }
}
