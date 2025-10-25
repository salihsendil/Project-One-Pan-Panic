using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private int currentGold = 9876;

    public void AddGold(int amount)
    {
        currentGold += amount;
    }

    public bool HasEnoughMoney(int cost)
    {
        if (currentGold >= cost)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

}
