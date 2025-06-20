using System.Collections;
using UnityEngine;
using Zenject;

public class OrderManager : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    [Inject] private GameStatsManager gameStatsManager;
    private OrderGenerator orderGenerator;
    private Coroutine orderCoroutine;

    private void Awake()
    {
        if (!TryGetComponent<OrderGenerator>(out orderGenerator))
        {
            Debug.LogError(this.name + " cannot find any Order Generator references!");
        }
    }

    private void Start()
    {
        orderCoroutine = StartCoroutine(GenerateOrder());
    }

    private IEnumerator GenerateOrder()
    {
        yield return new WaitForSeconds(gameManager.Settings.FirstOrderDelay);

        while (gameManager.Settings.MAX_ORDER_COUNT > gameStatsManager.CurrentOrderInstances.Count)
        {
            OrderInstance orderInstance = new OrderInstance();

            orderInstance.SetOrderInstance(orderGenerator.GenerateRandomRecipe());

            gameStatsManager.AddOrder(orderInstance);

            yield return new WaitForSeconds(Random.Range(gameManager.Settings.MinOrderDelay, gameManager.Settings.maxOrderDelay));
        }
    }

    private void DeleteOrder()
    {
        // expired or delivered doesnt matter



    }



}
