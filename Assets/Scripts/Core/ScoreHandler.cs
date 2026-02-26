using System;
using Zenject;

public class ScoreHandler : IInitializable, IDisposable
{
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;

    private int currentScore;

    public void Initialize()
    {
        SetScore(levelConfig.StartScore);

        signalBus.Subscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Subscribe<OrderExpiredSignal>(RemoveScore);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Unsubscribe<OrderExpiredSignal>(RemoveScore);
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
}