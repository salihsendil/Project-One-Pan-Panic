using Newtonsoft.Json;
using System;
using Zenject;

public class ScoreHandler : IInitializable, IDisposable, ISaveable
{
    [Inject] SaveSystem saveSystem;
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;
    [Inject] private LevelStatsService statsService;

    private int highScore;
    private int currentScore;

    public int HighScore => highScore;
    public SaveDataType GetSaveDataType => SaveDataType.Stats;


    public void Initialize()
    {
        SetScore(levelConfig.StartScore);
        LoadData();
        statsService.UpdateStats(StatsType.Score, currentScore);

        signalBus.Subscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Subscribe<OrderExpiredSignal>(RemoveScore);
        signalBus.Subscribe<GameFinishedSignal>(CheckNewHighScore);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Unsubscribe<OrderExpiredSignal>(RemoveScore);
        signalBus.Unsubscribe<GameFinishedSignal>(CheckNewHighScore);
    }

    public void SetScore(int score)
    {
        currentScore = score;
    }

    public int GetScore()
    {
        return currentScore;
    }

    private void AddScore(OrderDeliveredSignal signal)
    {
        currentScore += signal.Order.Recipe.SuccessScore;
        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private void RemoveScore(OrderExpiredSignal signal)
    {
        currentScore += signal.Order.Recipe.PenaltyScore;
        if (currentScore <= 0)
        {
            currentScore = 0;
        }
        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private void CheckNewHighScore()
    {
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
        StatsDataSave data = saveSystem.GetData<StatsDataSave>(GetSaveDataType);
        highScore = data.HighScore;
    }

    #endregion
}