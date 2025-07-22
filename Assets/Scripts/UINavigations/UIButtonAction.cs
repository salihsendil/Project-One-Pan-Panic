using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIButtonAction : MonoBehaviour
{
    private Button button;
    [Inject] private UICommandController commandController;
    [SerializeField] private UICommandSO uiCommandSO;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClick); 
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        commandController.GenericCommandInvoked(uiCommandSO);
    }

}
