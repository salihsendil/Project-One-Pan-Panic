using UnityEngine;
using UnityEngine.EventSystems;

public class OnScreenButtonPositioner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //References
    private RectTransform areaRectTransform;

    [Header("UI")]
    [SerializeField] private RectTransform buttonTransform;
    [SerializeField] private Vector2 buttonDefaultScreenPos;

    private void Start()
    {
        areaRectTransform = GetComponent<RectTransform>();
        buttonDefaultScreenPos = buttonTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(areaRectTransform,
                             eventData.position,
                             eventData.pressEventCamera,
                             out Vector2 screenPos);

        buttonTransform.anchoredPosition = screenPos;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonTransform.anchoredPosition = buttonDefaultScreenPos;
    }
}
