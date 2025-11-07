using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneData
{
    public ScenesEnum scene;
    public GameObject scenePrefab;
}

[CreateAssetMenu(fileName = "New UIConfigSO", menuName = "Scriptable Object/New UIConfigSO")]
public class UIConfigSO : ScriptableObject
{
    public List<SceneData> scenePrefabs = new List<SceneData>();

    public GameObject GetSceneUIPrefab(ScenesEnum scenesEnum)
    {
        SceneData data = scenePrefabs.Find(x => x.scene == scenesEnum);
        if (data != null)
        {
            return data.scenePrefab;
        }

        else
        {
            Debug.Log("sahne yok");
            return null;
        }
    }
}
