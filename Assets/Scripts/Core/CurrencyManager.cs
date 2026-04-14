using System;
using Zenject;

public class CurrencyManager : ISaveable, IInitializable
{
    //Zenject
    [Inject] private SaveSystem saveSystem;

    //Currency
    private int currentCurrency;

    //Event
    public event Action<int> OnCurrencyChanged;

    //Properties
    public int CurrentCurrency => currentCurrency;
    public SaveDataType GetSaveDataType => SaveDataType.PlayerData;


    public void Initialize()
    {
        LoadData();
    }

    private bool HasEnough(int amount)
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
        PlayerDataSave data = saveSystem.TryGetData<PlayerDataSave>(SaveDataType.PlayerData);
        data.Currency = currentCurrency;

        saveSystem.UpdateData(GetSaveDataType, data);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        PlayerDataSave data = saveSystem.TryGetData<PlayerDataSave>(GetSaveDataType);

        if (data == null) return;
        currentCurrency = data.Currency;
    }

    #endregion
}