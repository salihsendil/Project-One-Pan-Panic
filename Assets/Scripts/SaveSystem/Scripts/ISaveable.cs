public interface ISaveable
{
    public SaveDataType GetSaveDataType { get; }
    public string GetSaveData();
    public void LoadData(string json);
}
