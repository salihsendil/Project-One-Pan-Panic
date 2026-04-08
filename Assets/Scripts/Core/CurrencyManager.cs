using System;
using Zenject;

[Serializable]
public struct CurrencyData
{
    public int Currency;
}

public class CurrencyManager : ISaveable, IInitializable
{
    //Zenject
    [Inject] private SaveSystem saveSystem;

    //Currency
    private int currentCurrency = 2000;

    //Event
    public event Action<int> OnCurrencyChanged;

    //Properties
    public int CurrentCurrency => currentCurrency;
    public SaveDataType GetSaveDataType => SaveDataType.PlayerData;


    public void Initialize()
    {
        LoadData();
    }

    public bool HasEnough(int amount)
    {
        return currentCurrency >= amount;
    }

    public void Add(int amount)
    {
        currentCurrency += amount;
        SaveData();
        OnCurrencyChanged?.Invoke(currentCurrency);
    }

    public bool TrySpend(int amount)
    {
        if (HasEnough(amount))
        {
            currentCurrency -= amount;
            SaveData();
            OnCurrencyChanged?.Invoke(currentCurrency);
            return true;
        }
        return false;
    }

    #region SaveLoadData

    public void SaveData()
    {
        PlayerDataSave newData = new();
        newData.Currency = currentCurrency;

        saveSystem.UpdateData(GetSaveDataType, newData);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        PlayerDataSave data = saveSystem.GetData<PlayerDataSave>(GetSaveDataType);

        if (data == null) return;
        currentCurrency = data.Currency;
    }

    #endregion
}