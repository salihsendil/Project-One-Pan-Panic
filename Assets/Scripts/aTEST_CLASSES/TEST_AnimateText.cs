using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TEST_AnimateText : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text animatedText;
    [SerializeField] private int targetValue;
    [SerializeField] private float duration;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        animatedText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(AnimateText);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(AnimateText);
    }

    private void AnimateText(/*TMP_Text text*/)
    {
        animatedText.alpha = 0;
        rectTransform.localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(animatedText.DOFade(1f, 0.2f));
        sequence.Join(rectTransform.DOScale(Vector3.one * 1.8f, duration).SetEase(Ease.OutCubic));
        sequence.Join(rectTransform.DOLocalRotate(new Vector3(0f, 0f, 15f), duration).SetEase(Ease.OutCubic));
        sequence.Join(DOVirtual.Int(0, targetValue, duration, (currentValue) =>
        {
            animatedText.text = currentValue.ToString("N0");
        }).SetEase(Ease.OutQuad));

        sequence.AppendInterval(0.1f);

        sequence.Append(rectTransform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).SetEase(Ease.InExpo));

        sequence.AppendInterval(0.1f);
        sequence.Append(rectTransform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InExpo));
        sequence.Append(rectTransform.DOShakePosition(0.2f, 15f, 20, 1f));
    }

    [ContextMenu("Animate Text")]
    public void CallAnimate()
    {
        AnimateText();
    }



}
