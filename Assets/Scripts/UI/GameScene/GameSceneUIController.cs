using UnityEngine;
using Zenject;

public class GameSceneUIController : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    //Countdown
    [SerializeField] private CountdownDisplay countdownDisplay;

    private void OnEnable()
    {
        signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        signalBus.Subscribe<GameFinishedSignal>(OnGameFinished);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        signalBus.Unsubscribe<GameFinishedSignal>(OnGameFinished);
    }

    private void OnGameStarted()
    {
        countdownDisplay.Hide();
    }

    private void OnGameFinished()
    {
        countdownDisplay.Show();
    }
}