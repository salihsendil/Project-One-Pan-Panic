using Newtonsoft.Json;
using System;
using Zenject;

[Serializable]
public struct CurrencyData
{
    public int Currency;
}

public class CurrencyManager : ISaveable, IInitializable, IDisposable
{
    [Inject] private SaveSystem saveSystem;

    private int currentCurrency = 2000;

    public event Action<int> OnCurrencyChanged;

    public int CurrentCurrency => currentCurrency;

    public SaveDataType GetSaveDataType => SaveDataType.Currency;

    public void Initialize()
    {
        saveSystem.Register(this);
    }

    public void Dispose()
    {
        saveSystem.Unregister(this);
    }

    public bool HasEnough(int amount)
    {
        return currentCurrency >= amount;
    }

    public void Add(int amount)
    {
        currentCurrency += amount;
        OnCurrencyChanged?.Invoke(currentCurrency);
    }

    public bool TrySpend(int amount)
    {
        if (HasEnough(amount))
        {
            currentCurrency -= amount;
            OnCurrencyChanged?.Invoke(currentCurrency);
            return true;
        }
        return false;
    }

    public string GetSaveData()
    {
        CurrencyData currencyData = new CurrencyData();
        currencyData.Currency = currentCurrency;
        return JsonConvert.SerializeObject(currencyData, Formatting.Indented);
    }

    public void LoadData(string json)
    {
        CurrencyData currencyData = JsonConvert.DeserializeObject<CurrencyData>(json);
        currentCurrency = currencyData.Currency;
    }
}