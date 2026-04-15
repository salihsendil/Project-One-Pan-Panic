using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private List<Image> images = new List<Image>();

    private void Awake()
    {
        AllClear();
    }

    public void SetCanvasVisibility(bool isVisible)
    {
        canvasGroup.alpha = isVisible ? 1 : 0;
    }

    public void SetImage(Sprite sprite)
    {
        foreach (var image in images)
        {
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = sprite;
                break;
            }
        }
    }

    public void AllClear()
    {
        foreach (var image in images)
        {
            image.sprite = null;
            image.enabled = false;
        }

        SetCanvasVisibility(false);
    }
}
