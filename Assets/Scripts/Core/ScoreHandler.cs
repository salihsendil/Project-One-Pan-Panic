using System;
using Zenject;

public class ScoreHandler : IInitializable, IDisposable
{
    //Zenject
    [Inject] private SaveSystem saveSystem;
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;
    [Inject] private LevelStatsService levelStatsService;
    [Inject] private CurrencyManager currencyManager;
    [Inject] private LevelDataService levelDataService;

    //Score Variables
    private int currentScore;

    //References
    private ComboRewardManager comboManager;

    //Getters
    public int CurrentScore => currentScore;

    public void Initialize()
    {
        comboManager = new ComboRewardManager(levelConfig);

        signalBus.Subscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Subscribe<OrderExpiredSignal>(RemoveScore);
        signalBus.Subscribe<GameFinishedSignal>(HandleGameFinish);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Unsubscribe<OrderExpiredSignal>(RemoveScore);
        signalBus.Unsubscribe<GameFinishedSignal>(HandleGameFinish);
    }

    private void AddScore(OrderDeliveredSignal signal)
    {
        int successScore = signal.Order.Recipe.SuccessScore;
        currentScore += successScore + comboManager.GetRewardedScore(successScore);

        comboManager.UpdateComboState();

        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private void RemoveScore(OrderExpiredSignal signal)
    {
        comboManager.ResetScoreMultiplier();
        currentScore += signal.Penalty;
        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private void HandleGameFinish()
    {
        int earnedGold = comboManager.CalculateEarnedCurrency(currentScore);
        levelStatsService.UpdateStats(StatsType.EarnedGold, earnedGold);
        currencyManager.Add(earnedGold);

        levelDataService.LevelCompleted(currentScore);
    }
}