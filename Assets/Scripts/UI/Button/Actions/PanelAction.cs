using UnityEngine;

public class PanelAction : BaseUIAction
{
    [SerializeField] private GameObject panel;

    public override void Execute()
    {
        bool isOn = panel.activeSelf;
        panel.SetActive(!isOn);
    }
}
