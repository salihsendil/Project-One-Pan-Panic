using UnityEngine;
using UnityEngine.UI;


// This class shows related item process on the UI.


public class ProcessDisplay : MonoBehaviour, IProcessView
{
    [SerializeField] private BillboardCanvas billboard;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image fillBar;

    private void Awake()
    {
        Hide();
    }

    public void Initialize()
    {
        canvasGroup.alpha = 1;
        fillBar.fillAmount = 0;
        billboard.Register(canvasGroup);
    }

    public void Tick(float processRatio)
    {
        if (canvasGroup.alpha == 0) return;
        fillBar.fillAmount = processRatio;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        billboard.Unregister(canvasGroup);
    }
}
