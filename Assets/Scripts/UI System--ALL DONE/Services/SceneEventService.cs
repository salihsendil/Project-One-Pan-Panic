using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEventService : MonoBehaviour
{
    public static event Action<ScenesEnum> OnSceneChanged;
    public static Dictionary<string, ScenesEnum> sceneMap;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (sceneMap == null) { sceneMap = new(); }
        sceneMap.TryAdd(ScenesEnum.MainMenu.ToString(), ScenesEnum.MainMenu);
        sceneMap.TryAdd(ScenesEnum.PrototypeMap.ToString(), ScenesEnum.PrototypeMap);
        //sceneMap.Add(ScenesEnum.CustomizeScene.ToString(), ScenesEnum.CustomizeScene);

        SceneManager.sceneLoaded += HandleSceneChange;
    }

    private static void HandleSceneChange(Scene scene, LoadSceneMode loadMode)
    {
        if (sceneMap.TryGetValue(scene.name, out var scenesEnum))
        {
            OnSceneChanged?.Invoke(scenesEnum);
        }
        else
        {
            Debug.LogError($"SceneEventService: Unmapped scene {scene.name}");
        }
    }
}
