using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CustomizeDataWrapper
{
    public List<CustomizeData> dataList;

    public CustomizeDataWrapper(List<CustomizeData> dataList)
    {
        this.dataList = dataList;
    }
}

public class JsonDataHandler : MonoBehaviour
{
    private string dataSavePath;

    public static JsonDataHandler Instance { get; private set; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        dataSavePath = Path.Combine(Application.persistentDataPath + "/charCustomizationData.txt");

#if UNITY_EDITOR
        dataSavePath = Path.Combine(Application.dataPath + "/charCustomizationData.txt");
#endif

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SaveJson(CharacterCustomizationState.Instance.CustomizationHashList);
        }
    }

    public void SaveJson(HashSet<CustomizeData> hashData)
    {
        List<CustomizeData> customizes = new List<CustomizeData>();
        foreach (var data in hashData)
        {
            customizes.Add(data);
        }

        CustomizeDataWrapper dataWrapper = new CustomizeDataWrapper(customizes);

        foreach (var item in dataWrapper.dataList)
        {
            Debug.Log($"{item.bodyPart} + {item.meshId}");
        }

        string jsonOutput = JsonUtility.ToJson(dataWrapper, true);
        File.WriteAllText(dataSavePath, jsonOutput);

        Debug.Log($"Data succesfully saved to: {dataSavePath}");

    }

    public HashSet<CustomizeData> LoadJson()
    {
        if (!File.Exists(dataSavePath))
        {
            Debug.Log($"Data not found!");
            return new HashSet<CustomizeData>();
        }

        string jsonInput = File.ReadAllText(dataSavePath);
        CustomizeDataWrapper dataWrapper = JsonUtility.FromJson<CustomizeDataWrapper>(jsonInput);

        foreach (var item in dataWrapper.dataList)
        {
            Debug.Log($"{item.bodyPart} + {item.meshId}");
        }

        return new HashSet<CustomizeData>(dataWrapper.dataList);
    }

}










//private void ReSkinCharacter(CustomizeDataWrapper dataWrapper)
//{
//    foreach (var item in dataWrapper.dataList)
//    {
//        foreach (var data in CustomizationDataManager.Instance.Data.categories)
//        {
//            if (item.bodyPart == data.bodyPart)
//            {
//                if (string.IsNullOrWhiteSpace(item.meshId))
//                {
//                    CharacterCustomizer.Instance.ApplyData(data, 0);
//                }

//                else
//                {
//                    for (int i = 0; i < data.category.variants.Count; i++)
//                    {
//                        if (item.meshId == data.category.variants[i].id)
//                        {
//                            CharacterCustomizer.Instance.ApplyData(data, i);

//                        }
//                    }
//                }

//            }
//        }
//    }
//}


/* private void Update()
 {
     if (Input.GetKeyDown(KeyCode.T))
     {
         TestDebugList();
     }

     if (Input.GetKeyDown(KeyCode.J))
     {
         SaveJson();
         ExportJsonData();
     }

     if (Input.GetKeyDown(KeyCode.I))
     {
         ImportJsonData();
     }
 }

 private void TestDebugList()
 {
     List<CustomizeData> testData = new();
     foreach (var data in customizeHashData)
     {
         testData.Add(data);
     }

     foreach (var item in testData)
     {
         Debug.Log($"body part: {item.bodyPart} - item id: {item.meshId}");
     }

 }
*/
