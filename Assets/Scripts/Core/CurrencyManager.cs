using System;

public class CurrencyManager
{
    private int currentMoney = 2000;

    public event Action<int> OnCurrencyChanged;

    public int CurrentMoney => currentMoney;

    public bool HasEnough(int amount)
    {
        return currentMoney >= amount;
    }

    public void Add(int amount)
    {
        currentMoney += amount;
        OnCurrencyChanged?.Invoke(currentMoney);
    }

    public bool TrySpend(int amount)
    {
        if (HasEnough(amount))
        {
            currentMoney -= amount;
            OnCurrencyChanged?.Invoke(currentMoney);
            return true;
        }
        return false;
    }
}
