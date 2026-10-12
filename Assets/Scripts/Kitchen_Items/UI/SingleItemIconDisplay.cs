using UnityEngine;
using UnityEngine.UI;

public class SingleItemIconDisplay : MonoBehaviour, IItemIconDisplay
{
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        iconImage.enabled = true;
    }

    public void Set(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public void Hide()
    {
        iconImage.enabled = false;
    }
}
