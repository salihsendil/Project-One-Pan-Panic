using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerIconBillboarding : MonoBehaviour
{
    [SerializeField] private Canvas containerCanvas;
    [SerializeField] private List<Image> iconImages = new List<Image>();
    public Canvas ContainerCanvas { get => containerCanvas; }

    void Start()
    {
        ClearUI();
    }

    public void SetUIIconImage(Sprite icon)
    {
        containerCanvas.enabled = true;

        for (int i = 0; i < iconImages.Count; i++)
        {
            if (!iconImages[i].enabled)
            {
                iconImages[i].enabled = true;
                iconImages[i].sprite = icon;
                break;
            }
        }
    }

    public void ClearUI()
    {
        for (int i = 0; i < iconImages.Count; i++)
        {
            if (iconImages[i].enabled)
            {
                iconImages[i].enabled = false;
                iconImages[i].sprite = null;
            }

            containerCanvas.enabled = false;
        }
    }
}
