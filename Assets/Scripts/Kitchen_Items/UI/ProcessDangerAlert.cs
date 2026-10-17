using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProcessDangerAlert : MonoBehaviour, IProcessView
{
    [Header("Canvas")]
    [SerializeField] private BillboardCanvas billboard;

    [Header("Canvas")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image icon;

    [Header("Tween")]
    private Tween tween;
    [SerializeField] private float duration = 0.5f;


    private void Awake()
    {
        tween = icon
            .DOFade(0f, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .SetAutoKill(false)
            .Pause();
        
        Hide();
    }

    public void Initialize()
    {
        canvasGroup.alpha = 1;
        tween.Restart();
        billboard.Register(canvasGroup);
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
        billboard.Unregister(canvasGroup);
    }
}
