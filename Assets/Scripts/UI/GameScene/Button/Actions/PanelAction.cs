using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class PanelAction : BaseUIAction
{
    [SerializeField] private GameObject panel;

    public override void Execute()
    {
        bool willOpen = !panel.activeSelf;

        panel.transform.DOKill();

        if (willOpen)
        {
            panel.SetActive(true);
            panel.transform.localScale = Vector3.zero;

            panel.transform.DOScale(Vector3.one, 0.5f)
                 .SetEase(Ease.OutBack)
                 .SetUpdate(true);
        }

        else
        {
            panel.transform.DOScale(Vector3.zero, 0.3f)
                 .SetEase(Ease.InBack)
                 .SetUpdate(true)
                 .OnComplete(() => panel.SetActive(false));
        }
    }
}