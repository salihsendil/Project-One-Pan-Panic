public interface ISaveable
{
    public SaveDataType GetSaveDataType();
    public string GetSaveData();
    public void LoadData(string json);
}
