using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProcessDangerAlert : MonoBehaviour, IProcessView
{
    [Header("Canvas")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image icon;

    [Header("Tween")]
    private Tween tween;
    [SerializeField] private float duration = 0.5f;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        icon = GetComponent<Image>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        tween = icon
            .DOFade(0f, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .SetAutoKill(false)
            .Pause();

    }

    public void Initialize()
    {
        canvasGroup.alpha = 1;
        tween.Restart();
    }

    public void Tick(float processRatio)
    {
        if (canvasGroup.alpha == 0)
            return;

        float speedMultiplier = Mathf.Lerp(1f, 10f, processRatio);
        tween.timeScale = speedMultiplier;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        tween.timeScale = 1f;
        tween.Pause();
    }
}
