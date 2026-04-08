public interface ISaveable
{
    public SaveDataType GetSaveDataType { get; }
    public void SaveData();
    public void LoadData();
}
