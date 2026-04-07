using Newtonsoft.Json;
using System;
using Zenject;

public class ScoreHandler : IInitializable, IDisposable, ISaveable
{
    [Inject] SaveSystem saveSystem;
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;

    private int highScore;
    private int currentScore;

    public int HighScore => highScore;
    public SaveDataType GetSaveDataType => SaveDataType.Highscore;


    public void Initialize()
    {
        SetScore(levelConfig.StartScore);

        saveSystem.Register(this);
        signalBus.Subscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Subscribe<OrderExpiredSignal>(RemoveScore);
        signalBus.Subscribe<GameFinishedSignal>(CheckNewHighScore);
    }

    public void Dispose()
    {
        saveSystem.Unregister(this);
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
        if (currentScore > highScore)
            highScore = currentScore;
        UnityEngine.Debug.Log(highScore);
    }

    public string GetSaveData()
    {
        return JsonConvert.SerializeObject(highScore, Formatting.Indented);
    }

    public void LoadData(string json)
    {
        highScore = int.Parse(json);
    }
}