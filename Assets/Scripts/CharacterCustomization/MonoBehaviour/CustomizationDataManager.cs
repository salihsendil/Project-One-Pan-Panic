using UnityEngine;

public class CustomizationDataManager : MonoBehaviour
{
    [SerializeField] private CustomizationCategorySO data;

    public CustomizationCategorySO Data { get => data; }

    //singleton silcez!!

    public static CustomizationDataManager Instance { get; private set; }

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

    }



}
