using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;

    //Game State
    private GameplayPhase gameplayPhase = GameplayPhase.Countdown;

    //Timer
    private Timer timer = new();

    //Time Data
    private int countdownTime;
    private int gameTime;

    //Game Pause
    private bool isPaused;

    private void Awake()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    private void Start()
    {
        SetCountdown();

        gameTime = levelConfig.LevelTime;
        signalBus.Fire(new LevelTimerTickSignal(gameTime));
    }

    private void OnEnable()
    {
        signalBus.Subscribe<TogglePauseRequestSignal>(OnTogglePauseGame);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<TogglePauseRequestSignal>(OnTogglePauseGame);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        switch (gameplayPhase)
        {
            case GameplayPhase.Countdown:
                ProcessCountdownTimer(deltaTime);
                break;
            case GameplayPhase.Play:
                ProcessGameTimer(deltaTime);
                break;
            case GameplayPhase.Pause:
                break;
            case GameplayPhase.Finish:
                break;
            default:
                break;
        }
    }

    private void SetCountdown()
    {
        countdownTime = levelConfig.CountdownTime;
        countdownTime++;
        timer.Set(countdownTime);
    }

    private void ProcessCountdownTimer(float deltaTime)
    {
        timer?.Tick(deltaTime);
        int remainingInt = (int)timer.Remaining;

        if (countdownTime != remainingInt)
        {
            countdownTime = remainingInt;
            signalBus.Fire(new CountdownTickSignal(remainingInt));
        }

        if (timer.IsFinished)
        {
            timer.Reset();
            timer.Set(gameTime);
            gameplayPhase = GameplayPhase.Play;
            signalBus.Fire(new GameStartedSignal());
        }
    }

    private void ProcessGameTimer(float deltaTime)
    {
        timer?.Tick(deltaTime);
        int remainingInt = (int)timer.Remaining;

        if (gameTime != remainingInt)
        {
            gameTime = remainingInt;
            signalBus.Fire(new LevelTimerTickSignal(remainingInt));
        }

        if (timer.IsFinished)
        {
            timer.Reset();
            gameplayPhase = GameplayPhase.Finish;
            signalBus.Fire(new GameFinishedSignal());
        }
    }

    private void OnTogglePauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            gameplayPhase = GameplayPhase.Pause;
            return;
        }

        Time.timeScale = 1;
        gameplayPhase = GameplayPhase.Play;
    }
}
