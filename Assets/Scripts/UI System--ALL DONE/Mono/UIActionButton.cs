using System;
using UnityEngine;
using UnityEngine.UI;

public class UIActionButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private UIActionButtonSO actionButtonSO;
    public event Action<UIActionButtonSO> OnActionButtonClicked;

    private void Awake()
    {
        TryGetComponent(out button);
    }

    private void OnEnable()
    {
        if (button != null) { button.onClick.AddListener(PerformButtonAction); }
    }

    private void OnDisable()
    {
        if (button != null) { button.onClick.RemoveListener(PerformButtonAction); }
    }

    private void PerformButtonAction()
    {
        OnActionButtonClicked?.Invoke(actionButtonSO);
    }
}
