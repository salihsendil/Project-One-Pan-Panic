using UnityEngine;
using UnityEngine.UI;


// This class shows related item process on the UI.


public class ProcessDisplay : MonoBehaviour, IProcessView
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image fillBar;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Initialize()
    {
        canvasGroup.alpha = 1;
        fillBar.fillAmount = 0;
    }

    public void Tick(float processRatio)
    {
        if (canvasGroup.alpha == 0) return;
        fillBar.fillAmount = processRatio;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }
}
