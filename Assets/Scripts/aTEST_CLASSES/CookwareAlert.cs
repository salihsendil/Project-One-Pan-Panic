using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class CookwareAlert : MonoBehaviour, IProcessView
{
    [Header("Canvas")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image icon;

    [Header("Tween")]
    private Tween tween;
    [SerializeField] private float duration = 1f;

    [Header("Fake Value")]
    [Range(0, 1)]
    [SerializeField] private float fakeRatio = 0.01f;


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
        tween.Pause();
    }


    public void StartAlert()
    {
        canvasGroup.alpha = 1;
        tween.Restart();
    }

   
    public void TickAlert()
    {
        Tick(fakeRatio);
    }

    public void HideAlert()
    {
        Hide();
    }
}
