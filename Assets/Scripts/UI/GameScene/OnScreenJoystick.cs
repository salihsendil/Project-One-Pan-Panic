using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using Zenject;

public class OnScreenJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //References
    [Inject] private InputHandler inputHandler;
    private RectTransform areaRectTransform;

    [Header("Key Binding")]
    [InputControl(layout = "Vector2")]
    [SerializeField] private string controlPath = "<Gamepad>/leftStick";

    [Header("UI")]
    [SerializeField] private RectTransform padTransform;
    [SerializeField] private RectTransform nubTransform;
    [SerializeField] private Vector2 padDefaultScreenPos;

    [Header("Nub Settings")]
    [SerializeField] private float nubRange = 180f;

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(padTransform,
                             eventData.position,
                             eventData.pressEventCamera,
                             out Vector2 nubPos);

        nubPos = Vector2.ClampMagnitude(nubPos, nubRange);
        nubTransform.anchoredPosition = nubPos;
        inputHandler.SetMovementInput(nubPos / nubRange);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputHandler.SetMovementInput(Vector2.zero);
        padTransform.anchoredPosition = padDefaultScreenPos;
        nubTransform.anchoredPosition = Vector2.zero;
    }
}
