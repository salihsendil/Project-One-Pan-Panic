using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISliderHandler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private BaseUIAction[] uiActions;
    private BaseUIAction<float>[] uiActionsFloat;

    private void Awake()
    {
        if (slider == null) { TryGetComponent(out slider); }

        uiActions = GetComponents<BaseUIAction>();
        uiActionsFloat = GetComponents<BaseUIAction<float>>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        foreach (var action in uiActions)
        {
            if (action == null) { continue; }

            action.Execute();
        }

        foreach (var actionFloat in uiActionsFloat)
        {
            if (actionFloat == null) { continue; }

            actionFloat.Execute(value);
        }

    }
}