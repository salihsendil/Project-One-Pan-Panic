using System;
using Zenject;

public class ScoreHandler : IInitializable, IDisposable
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private LevelConfigSO levelConfig;

    //Score Variables
    private int currentScore = 0;
    private float comboMultiplier = 0f;

    //Getters
    public int CurrentScore => currentScore;

    public void Initialize()
    {
        signalBus.Subscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Subscribe<OrderExpiredSignal>(RemoveScore);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(AddScore);
        signalBus.Unsubscribe<OrderExpiredSignal>(RemoveScore);
    }

    private void AddScore(OrderDeliveredSignal signal)
    {
        int successScore = signal.Order.Recipe.SuccessScore;
        currentScore += GetComboScore(successScore);

        comboMultiplier += levelConfig.ComboMultiplierIncrement;

        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private void RemoveScore(OrderExpiredSignal signal)
    {
        comboMultiplier = 0f;
        currentScore += signal.Penalty;
        signalBus.Fire(new ScoreChangedSignal(currentScore));
    }

    private int GetComboScore(int successScore)
    {
        return successScore + (int)(currentScore * comboMultiplier);
    }
}