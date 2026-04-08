using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum StatsType { Score, CompleteOrder, FailOrder }

public class LevelStatsService : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    //Data
    public Dictionary<StatsType, int> statsDictionary = new Dictionary<StatsType, int>();


    private void OnEnable()
    {
        #region OrderSignals
        signalBus.Subscribe<OrderDeliveredSignal>(OrderDelivered);
        signalBus.Subscribe<OrderExpiredSignal>(OrderExpired);
        #endregion
    }

    private void OnDisable()
    {
        #region OrderSignals
        signalBus.Unsubscribe<OrderDeliveredSignal>(OrderDelivered);
        signalBus.Unsubscribe<OrderExpiredSignal>(OrderExpired);
        #endregion
    }

    #region OrderStats

    public void OrderDelivered(OrderDeliveredSignal signal)
    {
        UpdateStats(StatsType.CompleteOrder, 1);
        UpdateStats(StatsType.Score, signal.Order.Recipe.SuccessScore);

    }
    public void OrderExpired(OrderExpiredSignal signal)
    {
        UpdateStats(StatsType.FailOrder, 1);
        UpdateStats(StatsType.Score, signal.Order.Recipe.SuccessScore);
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
