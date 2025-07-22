using UnityEngine;

[CreateAssetMenu(fileName = "New Command", menuName = "Scriptable Object/New UI Command/New Scene Command")]
public class LoadSceneCommandSO : UICommandSO
{
    public SceneType sceneType;
    public override UICommandType CommandType { get; } = UICommandType.LoadScene;
}
