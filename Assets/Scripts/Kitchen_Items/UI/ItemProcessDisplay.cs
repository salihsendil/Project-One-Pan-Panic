using UnityEngine;
using UnityEngine.UI;

public class ItemProcessDisplay : MonoBehaviour, IProcessDisplayer
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
        //fillBar.fillAmount = Mathf.Lerp(fillBar.fillAmount, processRatio, Time.deltaTime * 500f);
        fillBar.fillAmount = processRatio;
    }

    public void Disable()
    {
        canvasGroup.alpha = 0;
    }
}
