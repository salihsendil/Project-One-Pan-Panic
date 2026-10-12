using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum StatsType { NotSpecified, Score = 5, CompleteOrder, FailOrder, EarnedGold }

public class LevelStatsService : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private LevelDataService levelDataService;

    [Header("Currency Calculation")]
    [SerializeField] private float currencyMultiplier = 0.04f;

    //Data
    private Dictionary<StatsType, int> statsDictionary = new();


    #region SignalSubscription

    private void OnEnable()
    {
        signalBus.Subscribe<OrderDeliveredSignal>(OrderDelivered);
        signalBus.Subscribe<OrderExpiredSignal>(OrderExpired);
        signalBus.Subscribe<ScoreChangedSignal>(ScoreChanged);
        //signalBus.Subscribe<GamePhaseChangedSignal>();
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(OrderDelivered);
        signalBus.Unsubscribe<OrderExpiredSignal>(OrderExpired);
        signalBus.Unsubscribe<ScoreChangedSignal>(ScoreChanged);
        //signalBus.Unsubscribe<GamePhaseChangedSignal>();
    }

    #endregion


    private void OrderDelivered(OrderDeliveredSignal signal)
    {
        int statValue = GetStat(StatsType.CompleteOrder);
        statValue++;
        UpdateStats(StatsType.CompleteOrder, statValue);
    }

    private void OrderExpired(OrderExpiredSignal signal)
    {
        int statValue = GetStat(StatsType.FailOrder);
        statValue++;
        UpdateStats(StatsType.FailOrder, statValue);
    }

    private void ScoreChanged(ScoreChangedSignal signal)
    {
        UpdateStats(StatsType.Score, signal.NewScore);

        int earnedGold = Mathf.FloorToInt(signal.NewScore * currencyMultiplier);
        UpdateStats(StatsType.EarnedGold, earnedGold);
    }

    private void UpdateStats(StatsType statsType, int newValue)
    {
        if (!statsDictionary.ContainsKey(statsType))
        {
            statsDictionary.Add(statsType, newValue);
        }

        statsDictionary[statsType] = newValue;
    }

    private void GameFinished()
    {
        int levelScore = GetStat(StatsType.Score);
        levelDataService.LevelCompleted(levelScore);
    }

    public int GetStat(StatsType statsType)
    {
        if (!statsDictionary.TryGetValue(statsType, out var value)) return 0;
        return value;
    }
}
