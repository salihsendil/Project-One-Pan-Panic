using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [Header("Config")]
    [SerializeField] private LevelConfigSO levelConfig;

    [Header("Timer")]
    private const float PREPARATION_TIME = 1.5f;
    [SerializeField] private Timer timer = new();
    [SerializeField] private int currentLevelTime;

    [Header("Game Phase")]
    [SerializeField] private GamePhase gamePhase;
    [SerializeField] private bool isPaused;

    [Header("UI Displays")]
    [SerializeField] private GameStatusDisplay statusDisplay;
    [SerializeField] private TimerDisplay timerDisplay;

    //Scene Service üzerinden scene fully loaded sinyalini dinleyip kamera blend hareketini ona göre yapabiliriz.

    public GamePhase GamePhase
    {
        get => gamePhase;
        set
        {
            if (gamePhase == value) return;
            gamePhase = value;
            SwitchGamePhase(value);
        }
    }

    private void Awake()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    private void Start()
    {
        GamePhase = GamePhase.Preparation;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        switch (GamePhase)
        {
            case GamePhase.Preparation:

                timer.Tick(deltaTime);
                if (timer.IsFinished)
                    GamePhase = levelConfig.StartPhase;
                break;

            case GamePhase.Countdown:

                timer.Tick(deltaTime);
                if (timer.IsFinished)
                    GamePhase = GamePhase.Play;
                break;

            case GamePhase.Play:

                timer.Tick(deltaTime);

                int remaningtime = Mathf.CeilToInt(timer.Remaining);
                if (remaningtime != currentLevelTime)
                {
                    currentLevelTime = remaningtime;
                    timerDisplay.UpdateTimerText(currentLevelTime);
                }

                if (timer.IsFinished)
                    GamePhase = GamePhase.Finish;
                break;
        }
    }

    private void SwitchGamePhase(GamePhase newPhase)
    {
        switch (newPhase)
        {
            case GamePhase.Preparation:
                statusDisplay.Hide();
                timer.Set(PREPARATION_TIME);
                break;

            case GamePhase.Countdown:
                timer.Set(levelConfig.CountdownTime);
                StartCoroutine(statusDisplay.ShowCountdown("Get Ready", "GO!", 3f));
                break;

            case GamePhase.Play:
                statusDisplay.Hide();
                timer.Set(levelConfig.LevelTime);
                break;

            case GamePhase.Finish:
                statusDisplay.Show("Time's UP!");
                break;
        }

        signalBus.Fire(new GamePhaseChangedSignal(newPhase));
    }

    public void TooglePause()
    {
        //gamephase finish/prep durumunda izin vermeyebiliriz.
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
