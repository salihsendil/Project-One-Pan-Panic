using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleItemIconDisplay : MonoBehaviour, IItemIconDisplay
{
    [SerializeField] private BillboardCanvas billboard;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private List<Image> iconImages;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        billboard.Register(canvasGroup);

        foreach (var image in iconImages)
        {
            if (!image.enabled && image.sprite != null)
            {
                image.enabled = true;
                break;
            }
        }
    }

    public void Set(Sprite sprite)
    {
        foreach (var image in iconImages)
        {
            if (image.sprite != null) continue;
            image.sprite = sprite;
            break;
        }
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        billboard.Unregister(canvasGroup);

        foreach (var image in iconImages)
        {
            image.sprite = null;
            image.enabled = false;
        }

    }
}
