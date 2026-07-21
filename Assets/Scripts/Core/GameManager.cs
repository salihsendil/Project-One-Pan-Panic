using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;

    //Game State
    [SerializeField] private GameplayPhase gameplayPhase;

    //Time Data
    private Timer timer = new();
    private int countdownTime;
    private int gameTime;

    //Game Pause
    private bool isPaused;

    private void Awake()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    private void OnEnable()
    {
        signalBus.Subscribe<TogglePauseRequestSignal>(OnTogglePauseGame);
        signalBus.Subscribe<SceneFullyLoadedSignal>(SceneLoaded);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<TogglePauseRequestSignal>(OnTogglePauseGame);
        signalBus.Unsubscribe<SceneFullyLoadedSignal>(SceneLoaded);
    }

    private void Start()
    {
        countdownTime = levelConfig.CountdownTime + 1;

        gameTime = levelConfig.LevelTime;
        signalBus.Fire(new LevelTimerTickSignal(gameTime));
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        switch (gameplayPhase)
        {
            case GameplayPhase.Countdown:
                ProcessTimer(deltaTime,
                    ref countdownTime,
                    remaining => signalBus.Fire(new CountdownTickSignal(remaining)),
                    GameplayPhase.Play);
                break;

            case GameplayPhase.Play:
                ProcessTimer(deltaTime,
                    ref gameTime,
                    remaining => signalBus.Fire(new LevelTimerTickSignal(remaining)),
                    GameplayPhase.Finish);
                break;

            default:
                break;
        }
    }

    private void SceneLoaded()
    {
        SetGameplayPhase(levelConfig.StartPhase);
    }

    private void ProcessTimer(float deltaTime, ref int lastReportedTime, Action<int> onTick, GameplayPhase nextPhase)
    {
        timer?.Tick(deltaTime);
        int remainingTime = (int)timer.Remaining;

        if (lastReportedTime != remainingTime)
        {
            lastReportedTime = remainingTime;
            onTick?.Invoke(remainingTime);
        }

        if (timer.IsFinished)
        {
            SetGameplayPhase(nextPhase);
        }
    }

    private void SetGameplayPhase(GameplayPhase nextPhase)
    {
        gameplayPhase = nextPhase;

        switch (gameplayPhase)
        {
            case GameplayPhase.Tutorial:
                inputHandler.SetInput(true);
                break;

            case GameplayPhase.Countdown:
                timer.Reset();
                timer.Set(countdownTime);
                signalBus.Fire(new CountdownStartedSignal());
                break;

            case GameplayPhase.Play:
                timer.Reset();
                timer.Set(gameTime);
                signalBus.Fire(new GameStartedSignal());
                break;

            case GameplayPhase.Finish:
                signalBus.Fire(new GameFinishedSignal());
                break;

            default:
                break;
        }
    }

    private void OnTogglePauseGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            SetGameplayPhase(GameplayPhase.Pause);
            return;
        }

        Time.timeScale = 1;
        SetGameplayPhase(GameplayPhase.Play);
    }

    public void SetPlayPhase() => SetGameplayPhase(GameplayPhase.Play);
}
