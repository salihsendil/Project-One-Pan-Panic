using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class PauseGameAction : BaseUIButtonAction
{
    [SerializeField] private GameManager gameManager;

    public override void Execute()
    {
        gameManager.TooglePause();
    }
}