using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
public class FloatingTextController : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text text;
    [SerializeField] private Vector3 startPos;

    [Header("Sequence")]
    private Sequence sequence;
    [SerializeField] private float lifeTime = 0.7f;

    [Header("Fade")]
    [SerializeField] private float fadeInOutTime = 0.3f;

    [Header("Position Move")]
    [SerializeField] private float localMoveTime = 0.3f;
    [SerializeField] private float localMoveY = 0.7f;

    [Header("Shake")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private Vector2 shakeStrengh = new(0.1f, 0f);
    [SerializeField] private int vibration = 20;


    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.alpha = 0;

        startPos = text.rectTransform.localPosition;

        sequence = DOTween.Sequence().SetAutoKill(false).Pause();
        sequence.Join(text.DOFade(1, fadeInOutTime));
        sequence.Join(text.rectTransform.DOLocalMoveY(startPos.y + localMoveY, localMoveTime)).SetEase(Ease.InOutQuad);
        sequence.Append(text.rectTransform.DOShakeAnchorPos(shakeDuration, shakeStrengh, vibration, 0f));

        sequence.AppendInterval(lifeTime);
        sequence.Append(text.DOFade(0f, fadeInOutTime));
    }

    public void Show(string message)
    {
        text.text = message;
        text.DOKill();
        text.rectTransform.localPosition = startPos;
        sequence.Restart();
    }

    [ContextMenu("Test Et")]
    public void TestButtonClick()
    {
        Show("!!!!");
    }
}
