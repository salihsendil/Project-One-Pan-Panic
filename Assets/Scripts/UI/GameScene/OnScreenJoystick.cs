using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class OnScreenJoystick : OnScreenControl, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Transform")]
    private RectTransform areaRectTransform;

    [Header("Key Binding")]
    [InputControl(layout = "Vector2")]
    [SerializeField] private string bindingPath = "<Gamepad>/leftStick";

    [Header("UI")]
    [SerializeField] private RectTransform padTransform;
    [SerializeField] private RectTransform nubTransform;
    [SerializeField] private Vector2 padDefaultScreenPos;

    [Header("Nub Settings")]
    [SerializeField] private float nubRange = 170f;

    protected override string controlPathInternal
    {
        get => bindingPath;
        set => bindingPath = value;
    }

    private void Start()
    {
        areaRectTransform = GetComponent<RectTransform>();
        padDefaultScreenPos = padTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(areaRectTransform,
                             eventData.position,
                             eventData.pressEventCamera,
                             out Vector2 screenPos);

        padTransform.anchoredPosition = screenPos;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(padTransform,
                             eventData.position,
                             eventData.pressEventCamera,
                             out Vector2 inputVector);

        inputVector = Vector2.ClampMagnitude(inputVector, nubRange);
        nubTransform.anchoredPosition = inputVector;
        SendValueToControl(inputVector);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SendValueToControl(Vector2.zero);
        padTransform.anchoredPosition = padDefaultScreenPos;
        nubTransform.anchoredPosition = Vector2.zero;
    }
}
