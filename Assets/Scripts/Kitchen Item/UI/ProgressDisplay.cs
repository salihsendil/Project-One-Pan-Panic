using UnityEngine;
using UnityEngine.UI;

public class ProgressDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Image fillBar;
    [SerializeField] private Image warningImage;

    private Color32 defaultColor = new(5, 175, 25, 255);
    private Color32 warningColor = Color.red;


    private float duration;
    private float fillAmount;

    private void OnEnable()
    {
        fillBar.color = defaultColor;
        canvas.alpha = 0;
    }

    private void OnDisable()
    {
        Hide();
        ClearBar();
    }

    public void InitializeBar(float duration, bool isWarning = false)
    {
        if (isWarning)
        {
            SetWarningIcon(isWarning);
            fillBar.color = warningColor;
        }

        canvas.alpha = 1;
        this.duration = duration;
    }

    public void UpdateBar(float value)
    {
        fillAmount += value;
        fillBar.fillAmount = fillAmount / duration;
    }

    public void Hide()
    {
        SetWarningIcon(false);
        canvas.alpha = 0;
    }

    public void ClearBar()
    {
        fillBar.color = defaultColor;
        canvas.alpha = 0;
        duration = fillAmount = 0;
    }

    public void SetWarningIcon(bool isVisible)
    {
        if (warningImage != null)
            warningImage.enabled = isVisible;
    }
}
