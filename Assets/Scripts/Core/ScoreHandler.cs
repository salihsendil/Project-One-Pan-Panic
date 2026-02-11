using System;
using Zenject;

public class ScoreHandler : IInitializable, IDisposable
{
    [Inject] private SignalBus signalBus;

    private int currentScore;

    public void Initialize()
    {
        signalBus.Subscribe<OrderDeliveredSignal>(AddScore);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(AddScore);
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
        currentScore += signal.SuccessScore;
        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private void RemoveScore(int amount)
    {
        currentScore -= amount;
        if (currentScore <= 0)
        {
            currentScore = 0;
        }
        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }
}
