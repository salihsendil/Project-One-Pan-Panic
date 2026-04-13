using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum StatsType { Score, CompleteOrder, FailOrder, EarnedGold }

public class LevelStatsService : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    //Data
    public Dictionary<StatsType, int> statsDictionary = new Dictionary<StatsType, int>();

    #region SignalSubscription

    private void OnEnable()
    {
        signalBus.Subscribe<OrderDeliveredSignal>(OrderDelivered);
        signalBus.Subscribe<OrderExpiredSignal>(OrderExpired);
        signalBus.Subscribe<ScoreChangedSignal>(ScoreChanged);

    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(OrderDelivered);
        signalBus.Unsubscribe<OrderExpiredSignal>(OrderExpired);
        signalBus.Unsubscribe<ScoreChangedSignal>(ScoreChanged);
    }

    #endregion

    #region OrderStats

    private void OrderDelivered(OrderDeliveredSignal signal)
    {
        UpdateStats(StatsType.CompleteOrder, 1);
    }
    private void OrderExpired(OrderExpiredSignal signal)
    {
        UpdateStats(StatsType.FailOrder, 1);
    }
    private void ScoreChanged(ScoreChangedSignal signal)
    {
        UpdateStats(StatsType.Score, signal.NewScore);
    }

    #endregion

    public void UpdateStats(StatsType statsType, int amount)
    {
        Debug.Log(statsType);
        if (!statsDictionary.TryGetValue(statsType, out int value))
            statsDictionary.Add(statsType, value);

        int newValue = value + amount;
        statsDictionary[statsType] = newValue;
    }

    public int GetStat(StatsType statsType)
    {
        if (!statsDictionary.TryGetValue(statsType, out var value)) return 0;
        return value;
    }
}
