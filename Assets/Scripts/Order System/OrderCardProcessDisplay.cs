using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OrderCardProcessDisplay : MonoBehaviour
{
    //References
    private RectTransform rectTransform;
    [SerializeField] private Image fillBar;

    //Warning Variables
    private bool isShaking;
    [SerializeField] private float timeShakeThreshold = 0.3f;
    [SerializeField] private Color32 baseColor;
    [SerializeField] private Color32 warningColor;

    //Time
    [SerializeField] private float duration;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Set(float duration)
    {
        this.duration = duration;
        fillBar.fillAmount = 1;
        StopShake();
    }

    public void Tick(float remaining)
    {
        float percent = remaining / duration;
        fillBar.fillAmount = percent;
        fillBar.color = Color.Lerp(warningColor, baseColor, percent);

        if (!isShaking && percent < timeShakeThreshold)
        {
            ShakePanel();
        }
    }

    public void Disable()
    {
        StopShake();
    }

    private void ShakePanel()
    {
        isShaking = true;
        rectTransform.DOShakeAnchorPos(0.5f, 5f, 10, 60).SetLoops(-1, LoopType.Restart);
    }

    private void StopShake()
    {
        rectTransform.DOKill();
        isShaking = false;
    }
}
