using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialIconDisplay : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Image tutorialIcon;

    private Tween floatTween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        tutorialIcon = GetComponent<Image>();
        Show();
    }

    public void Show()
    {
        floatTween?.Kill();

        StartFloatAnimation();
    }

    public void Hide()
    {
        floatTween?.Kill();
    }

    private void StartFloatAnimation()
    {
        floatTween = rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 0.25f, 1f)
            .SetEase(Ease.InOutQuad) // Yumuþak bir kalkýþ ve duruþ saðlar (fiziksel his verir)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
