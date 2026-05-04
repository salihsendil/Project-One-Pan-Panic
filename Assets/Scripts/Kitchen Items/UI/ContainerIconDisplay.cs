using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerIconDisplay : MonoBehaviour
{
    [SerializeField] private List<Image> icons;

    public void ShowIcon(Sprite itemIcon)
    {
        foreach (var icon in icons)
        {
            if (icon.gameObject.activeSelf) continue;

            icon.gameObject.SetActive(true);
            icon.enabled = true;
            icon.sprite = itemIcon;
            break;
        }
    }

    public void Hide()
    {
        foreach (var icon in icons)
        {
            icon.gameObject.SetActive(false);
            icon.enabled = false;
        }
    }
}
