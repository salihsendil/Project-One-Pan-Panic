using UnityEngine;
using Zenject;

public class OrderTimerController : MonoBehaviour
{
    [Inject] private GameStatsManager gameStats;

    void Update()
    {
        for (int i = 0; i < gameStats.CurrentOrderInstances.Count; i++)
        {
            gameStats.CurrentOrderInstances[i].Tick(Time.deltaTime);
        }
    }
}
