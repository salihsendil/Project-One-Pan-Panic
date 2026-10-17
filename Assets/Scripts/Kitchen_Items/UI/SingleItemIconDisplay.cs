using UnityEngine;
using UnityEngine.UI;

public class SingleItemIconDisplay : MonoBehaviour, IItemIconDisplay
{
    [SerializeField] private BillboardCanvas billboard;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        iconImage.enabled = true;
        billboard.Register(canvasGroup);
    }

    public void Set(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        iconImage.enabled = false;
        billboard.Unregister(canvasGroup);
    }
}
