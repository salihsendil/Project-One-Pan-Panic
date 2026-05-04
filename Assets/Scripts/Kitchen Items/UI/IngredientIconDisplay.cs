using UnityEngine;
using UnityEngine.UI;

public class IngredientIconDisplay : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void Show()
    {
        icon.enabled = true;
    }

    public void SetIcon(Sprite itemIcon)
    {
        icon.sprite = itemIcon;
    }

    public void Hide()
    {
        icon.enabled = false;
    }
}
