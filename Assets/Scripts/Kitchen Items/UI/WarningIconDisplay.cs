using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WarningIconDisplay : MonoBehaviour, IProcessDisplayer
{
    //Icon Reference
    [SerializeField] private Image icon;

    //Animation Settings
    private Tween fadeTween;
    private float duration = 1f; //Pulse Duration

    private void Awake()
    {
        icon.enabled = false;
    }
    public void Initialize()
    {
        icon.enabled = true;
        StartFadeAnimation();
    }

    private void StartFadeAnimation()
    {
        fadeTween?.Kill();

        icon.color = Color.white;
        fadeTween = icon.DOFade(0.1f, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Tick(float value)
    {
        float speedMultiplier = Mathf.Lerp(1f, 8f, value);
        fadeTween.timeScale = speedMultiplier;
    }

    public void Disable()
    {
        fadeTween?.Kill();
        icon.enabled = false;
    }
}
