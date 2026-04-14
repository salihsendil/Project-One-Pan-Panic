using System;
using Zenject;

public class ScoreHandler : IInitializable, IDisposable, ISaveable
{
    //Zenject
    [Inject] private SaveSystem saveSystem;
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;
    [Inject] private LevelStatsService statsService;
    [Inject] private CurrencyManager currencyManager;

    //Score Variables
    private int highScore;
    private int currentScore;

    ////References
    private ComboRewardManager comboManager;

    //Getters
    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public int HighScore => highScore;
    public SaveDataType GetSaveDataType => SaveDataType.Stats;

    public void Initialize()
    {
        LoadData();
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
        currentScore += signal.Order.Recipe.PenaltyScore;

        if (currentScore <= 0)
            currentScore = 0;

        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private void HandleGameFinish()
    {
        int earnedGold = comboManager.GetEarnedCurrency(currentScore);
        statsService.UpdateStats(StatsType.EarnedGold, earnedGold);
        currencyManager.Add(earnedGold);

        if (currentScore <= highScore) return;
        highScore = currentScore;
        SaveData();
    }

    #region SaveLoadData

    public void SaveData()
    {
        StatsDataSave newData = new();
        newData.HighScore = highScore;
        saveSystem.UpdateData(GetSaveDataType, newData);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        StatsDataSave data = saveSystem.TryGetData<StatsDataSave>(GetSaveDataType);
        highScore = data.HighScore;
    }

    #endregion
}